using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDamagePresenter : MonoBehaviour
{
    private Health _health;
    private void Awake()
    {
        _health = GetComponent<Health>();
    }
    private void OnEnable()
    {
        _health.OnDamageTaken += OnDamageTaken;
    }
    private void OnDisable()
    {
        _health.OnDamageTaken -= OnDamageTaken;
    }

    private void OnDamageTaken(float amount)
    {
        var damageTransform = ObjectPools.Instance.damageIndicatorPool.GetNext();
        damageTransform.GetComponentInChildren<TextMeshPro>().text = amount.ToString();
        damageTransform.position = transform.position + (Vector3.right* UnityEngine.Random.Range(-1,1));
    }
}
