using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : NetworkBehaviour
{
    public PlayerController playerController;
    public PlayerAttack playerAttack;
    public Health health;

    [SerializeField] private Image _ownerIcon;
    public SpriteRenderer hostIndicator;

    private void Start()
    {
        if (IsOwner)
        {
            _ownerIcon.gameObject.SetActive(true);
        }
        if(IsOwnedByServer)
        {
            hostIndicator.color = Color.green;
        }
        else
        {
            hostIndicator.color = Color.blue;
        }
      
    }

    private void OnEnable()
    {
        GameNetworkManager.Instance.PlayerSpawned(this);
    }

    private void OnDisable()
    {
        if(GameNetworkManager.Instance)
            GameNetworkManager.Instance.PlayerDespawned(this);
    }
}
