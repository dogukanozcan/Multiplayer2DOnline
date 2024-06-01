using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MapEventManager : MonoBehaviour
{
    [SerializeField] private NetworkObject _bombObject;
    [SerializeField] private float _spawnInterval = 2.5f;
    private NetworkManager _networkManager;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        _networkManager = NetworkManager.Singleton;
        yield return new WaitUntil(() => _networkManager.IsConnectedClient);
        if (_networkManager.IsHost)
        {
            StartCoroutine(EnemySpawner());
        }
    }

    public IEnumerator EnemySpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);
            yield return new WaitUntil(() => GameNetworkManager.IsGameStarted());
            SpawnEnemy();
            
        }
    }

    public void SpawnEnemy()
    {
        var instance = Instantiate(_bombObject, SpawnPointManager.Instance.GetMapEventSpawnPoint(), Quaternion.identity);
        instance.Spawn(true);
    }
}
