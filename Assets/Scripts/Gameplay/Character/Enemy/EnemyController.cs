using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }


 

    // Update is called once per frame
    void FixedUpdate()
    {
        var nearst = GetNearstPlayer();
        if (nearst != null)
        {
            _agent.SetDestination(nearst.position);
        }
        
    }

    public Transform GetNearstPlayer()
    {
        var players = GameNetworkManager.Instance.spawnedPlayers;
        if(players.Count == 0) return null;

        if (players.Count == 1)
        {
            return players[0].transform;
        }
        else
        {
            var nearstDistance = float.MaxValue;
            var nearstPlayer = players[0];
            foreach(var p in players)
            {
                var distance = Vector2.Distance(p.transform.position, transform.position);
                if(distance < nearstDistance)
                {
                    nearstDistance = distance;
                    nearstPlayer = p;
                }
            }
            return nearstPlayer.transform;
        }
    }
}
