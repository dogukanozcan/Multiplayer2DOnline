using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Multiplayer.Playmode;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class Matchmaking: MonoBehaviour
{
    private NetworkManager _networkManager;
    private bool _isClientStarted;

    private IEnumerator Start()
    {
        _networkManager = NetworkManager.Singleton;
        _networkManager.OnClientStarted += _networkManager_OnClientStarted; 
        var randomTime = UnityEngine.Random.Range(0, 1f);
        yield return new WaitForSeconds(randomTime);
        StartCoroutine(CreateOrJoinLobby());
    }

    private void _networkManager_OnClientStarted() => _isClientStarted = true;

    public IEnumerator CreateOrJoinLobby()
    {
        _networkManager.StartClient();
        yield return new WaitUntil(() => _isClientStarted);
        var randomTime = UnityEngine.Random.Range(0, 2.5f);
        yield return new WaitForSeconds(randomTime);
        if (!_networkManager.IsConnectedClient)
        {
            _networkManager.Shutdown();
            yield return new WaitWhile(() => _networkManager.ShutdownInProgress);
            _networkManager.StartHost();
        }
    }
}