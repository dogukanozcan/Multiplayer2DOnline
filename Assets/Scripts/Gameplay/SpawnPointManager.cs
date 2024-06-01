using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoSingleton<SpawnPointManager>
{
    public Transform hostSpawnPoint;
    public Transform clientSpawnPoint;
    [SerializeField] private Transform mapEventSpawnArea;

    public Vector2 GetMapEventSpawnPoint()
    {
        var width = mapEventSpawnArea.localScale.x;
        var height = mapEventSpawnArea.localScale.y;
        var randomX = Random.Range(-width/2f, width/2f);
        var randomY = Random.Range(-height / 2f, height / 2f);
        return new Vector2(randomX, randomY);
    }
    public Vector3 GetPlayerSpawnPoint(ulong id)
    {
        //print("ConnectionApprovalCallback id: " + id);
        switch (id)
        {
            case 0:
                return hostSpawnPoint.position;
            case 1:
                return clientSpawnPoint.position;
            default:
                return Vector3.zero;
        }
    }
}
