using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
public class Health : NetworkBehaviour, IAttackable
{
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isInvulnerable;

    [SerializeField] private bool _startInvulnerable = false;
    private bool _isDead;

    public Action<float> OnHealthChange;
    public Action<float> OnDamageTaken;
    public Action<float> OnMaxHealthChange;

    public void Awake()
    {
        currentHealth = maxHealth;

        if (_startInvulnerable)
        {
            isInvulnerable = true;
            Invoke(nameof(SetVulnerable), 2f);
        }
        else
        {
            SetVulnerable();
        }
    }

    public void SetVulnerable()
    {
        isInvulnerable = false;
    }


    [Rpc(SendTo.Everyone)]
    public void DamageRpc(float amount)
    {
        if (_isDead || isInvulnerable || !GameNetworkManager.IsGameStarted())
            return;

        OnDamageTaken?.Invoke(amount);
        ChangeHealth(currentHealth-amount);
        if( currentHealth <= 0)
        {
            ChangeHealth(0);
            Die();
        }
            
    }

    public void ChangeHealth(float newHealth)
    {
        currentHealth = newHealth;
        OnHealthChange?.Invoke(currentHealth);
    }

    public void Die()
    {
        _isDead = true;
        gameObject.SetActive(false);
        if (IsHost)
        {
            Destroy(gameObject,1f);
        }
    }

    
}
