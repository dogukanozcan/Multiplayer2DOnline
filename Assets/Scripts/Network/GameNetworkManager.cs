using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using System.Net.NetworkInformation;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Core;
using Unity.Services.Authentication;

public class GameNetworkManager : MonoSingleton<GameNetworkManager>
{
    public event Action OnGameEnd;
    public List<PlayerManager> spawnedPlayers = new();
    private NetworkManager _networkManager;

    private void Start()
    {
        _networkManager = NetworkManager.Singleton;
        _networkManager.NetworkConfig.ConnectionApproval = true;
        _networkManager.ConnectionApprovalCallback += ConnectionApprovalCallback;
    }

    private void OnDisable()
    {
        _networkManager.Shutdown();
        _networkManager.ConnectionApprovalCallback -= ConnectionApprovalCallback;
    }

    public void ConnectionApprovalCallback(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        response.Position = SpawnPointManager.Instance.GetPlayerSpawnPoint(request.ClientNetworkId);
        response.Approved = true;
        response.CreatePlayerObject = true;
    }
    public static bool IsGameStarted() => Instance.spawnedPlayers.Count >= 2;

    public void PlayerSpawned(PlayerManager playerManager)
    {
        spawnedPlayers.Add(playerManager);
    }

    public void PlayerDespawned(PlayerManager playerManager)
    {
        spawnedPlayers.Remove(playerManager);
        if(spawnedPlayers.Count == 1)
        {
            OnGameEnd?.Invoke();
        }
    }

}
