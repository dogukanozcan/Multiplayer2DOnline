using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image healthbar;
    [SerializeField] private Image healthbarEffect;

    private PlayerManager playerManager;

    private float _maxHealth;
    private TweenerCore<float, float, FloatOptions> tween;
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    private void OnEnable()
    {
        playerManager.health.OnHealthChange += OnHealthChange;
        _maxHealth = playerManager.health.maxHealth;
    }
    private void OnDisable()
    {
        playerManager.health.OnHealthChange -= OnHealthChange;
        tween?.Kill();
    }

    private void OnDestroy()
    {
        playerManager.health.OnHealthChange -= OnHealthChange;
        tween?.Kill();
    }
    private void OnHealthChange(float newHealth)
    {
        UpdateHealtbar(newHealth);
    }

    public void UpdateHealtbar(float newHealth)
    {
        float percent = newHealth / _maxHealth;
        healthbar.fillAmount = percent;
        var animationTime = 0.4f;
        if(healthbarEffect != null)
        {
            tween = healthbarEffect.DOFillAmount(percent, animationTime);
        }
           
    }

   

}
