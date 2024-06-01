using UnityEngine;

namespace Unity.Netcode.Samples
{
    public class NetworkDebugManager : MonoBehaviour
    {
        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));

            var networkManager = NetworkManager.Singleton;
            if (networkManager.IsClient || networkManager.IsServer)
            {
                GUILayout.Label($"Mode: {(networkManager.IsHost ? "Host" : networkManager.IsServer ? "Server" : "Client")}");
            }

            GUILayout.EndArea();
        }
    }
}
