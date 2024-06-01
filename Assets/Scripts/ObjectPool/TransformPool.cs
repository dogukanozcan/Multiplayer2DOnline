using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPool : MonoBehaviour
{
    [SerializeField] private Transform _transformPrefab;
    [SerializeField] private int poolSize = 50;
    private List<Transform> _objects = new();

    private void Awake()
    {
        for(int i = 0; i < poolSize; i++)
        {
            CreateObject();
        }
    }
    public Transform GetNext()
    {
        var next = _objects.Find((obj) => !obj.gameObject.activeInHierarchy);
        if (next == null)
        {
            next = CreateObject();
        }
        next.gameObject.SetActive(true);
        return next;
    }
    public Transform CreateObject()
    {
        var obj = Instantiate(_transformPrefab, transform);
        obj.gameObject.SetActive(false);
        _objects.Add(obj);
        return obj;
    }
}
