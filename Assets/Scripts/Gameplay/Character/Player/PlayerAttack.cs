using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class PlayerAttack : MonoNetworkDelay
{
    public float attackRange = 1.4f;
    public float attackSpeed = 1f;
    public float damage = 10f;
    public LayerMask enemyLayerMask;

    public Action<Vector2, float> OnAttackStart;
    public Action OnAttackStop;

    private float lastAttackTime = 0f;
    private Collider2D _collider;
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        lastAttackTime = -1f / attackSpeed;
    } 

    private void Start()
    {
       
    }
    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        var enemyList = Physics2D.OverlapCircleAll(transform.position, attackRange / 2f, enemyLayerMask).ToList();
        enemyList.Remove(_collider);
        if (enemyList.Count >= 1)
        {
            var enemy = enemyList[0];
            if (enemy == _collider)
                return;

            var diffToEnemy = enemy.transform.position - transform.position;
            var dirToEnemy = diffToEnemy.normalized;
            if (Time.time - lastAttackTime >= 1f/attackSpeed)
            {
                if (IsHost)
                {
                    var tinyDelayForAttackProc = (1f / attackSpeed) / 2F;
                    Delay(() => AttackCommand(enemy), tinyDelayForAttackProc);
                }

                lastAttackTime = Time.time;
            }
            OnAttackStart?.Invoke(dirToEnemy, attackSpeed);
        }
        else
        {
            OnAttackStop?.Invoke();
        }
        
    }

    public void AttackCommand(Collider2D enemy)
    {
        if(enemy != null)
        {
            var attackable = enemy.GetComponent<IAttackable>();
            attackable?.DamageRpc(damage);
        }
    }
}
