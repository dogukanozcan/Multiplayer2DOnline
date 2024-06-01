using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyDetonatePresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    private EnemyAttack _enemyAttack;

    private void Awake()
    {
        _enemyAttack = GetComponent<EnemyAttack>();
    }
    private void OnEnable()
    {
        _enemyAttack.OnDetonateTimeChange += OnDetonateTimeChange;
    }
    private void OnDisable()
    {
        _enemyAttack.OnDetonateTimeChange -= OnDetonateTimeChange;
    }

    private void OnDetonateTimeChange(float newTime)
    {
        if(newTime <= 1)
        {
            timer.color = Color.red;
        }
        else
        {
            timer.color = Color.white;
        }

        timer.text = newTime.ToString("0.00");
    }
}
