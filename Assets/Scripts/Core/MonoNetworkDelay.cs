using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class MonoNetworkDelay : NetworkBehaviour
{
    public void Delay(Action action, float delay)
    {
        StartCoroutine(Delay_Handler(action, delay));
    }
    private IEnumerator Delay_Handler(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();

    }
}
