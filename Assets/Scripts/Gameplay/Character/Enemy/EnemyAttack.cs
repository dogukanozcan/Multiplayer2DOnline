using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyAttack : NetworkBehaviour
{
    public Action<float> OnDetonateTimeChange;
    [SerializeField] private float _attackRange = 3f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _explodeTime = 1.5f;

    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeField] private SpriteRenderer _attackRangeIndicator;
    [SerializeField] private GameObject _explodeEffect;

    private float _detonateCountdown = 0f;
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _attackRangeIndicator.transform.localScale = Vector3.one*_attackRange;
        _detonateCountdown = _explodeTime;
    }
    public void Update()
    {
        Attack();
    }
    private void OnEnable()
    {
        GameNetworkManager.Instance.OnGameEnd += OnGameEnd;
    }
    private void OnDisable()
    {
        GameNetworkManager.Instance.OnGameEnd -= OnGameEnd;
    }


    public void Attack()
    {
        var enemyList = Physics2D.OverlapCircleAll(transform.position, _attackRange / 2f, _enemyLayerMask).ToList();
        enemyList.Remove(_collider);

        ChangeDetonateTime(_detonateCountdown - Time.deltaTime);
        if (_detonateCountdown < 0f)
        {
            if (enemyList.Count >= 1)
            {
                var enemy = enemyList[0];
                Explode(enemy);
            }
            else
            {
                Explode(null);
            }
        }
        
    }
    private void OnGameEnd()
    {
        Explode(null);
    }
    public void ChangeDetonateTime(float newValue)
    {
        _detonateCountdown = newValue;
        OnDetonateTimeChange?.Invoke(newValue);
    }
    public void Explode(Collider2D enemy)
    {
        if (enemy)
        {
            if (IsHost)
            {
                var attackable = enemy.GetComponent<IAttackable>();
                attackable?.DamageRpc(_damage);
            }
        }
        _detonateCountdown = _explodeTime;
        if (_explodeEffect)
        {
            _explodeEffect.SetActive(true);
            _explodeEffect.transform.SetParent(null);
        }
        gameObject.SetActive(false);
        if (IsHost)
        {
            Destroy(gameObject,.1f);
        }
    }
}
