using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroyer : MonoBehaviour
{
    [SerializeField] private float _destroyTime = 1.0f;
    [SerializeField] private bool _destroy = true;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        if (_animator != null)
        {
            _destroyTime = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        }
    }
    private void OnEnable()
    {
        StartCoroutine(DestroyHandle());
    }

    IEnumerator DestroyHandle()
    {
        yield return new WaitForSeconds(_destroyTime);
        if (_destroy)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
