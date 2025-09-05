using Unity.Netcode;
using UnityEngine;

namespace Son
{
    public class NetworkHandler : MonoBehaviour
    {
        [SerializeField] private int maxPlayers = 4;
        
        private NetworkManager networkManager;
        
        private void Awake()
        {
            networkManager = GetComponent<NetworkManager>();
        }

        private void OnEnable()
        {
            Debug.Log("OnEnable");
            networkManager.ConnectionApprovalCallback = CallApprovalCheck;
            networkManager.OnClientConnectedCallback += CallClientConnectedCallback;
        }

        private void OnDisable()
        {
            Debug.Log("OnDisable");
            networkManager.ConnectionApprovalCallback = null;
            networkManager.OnClientConnectedCallback -= CallClientConnectedCallback;
        }

        private void CallApprovalCheck(
            NetworkManager.ConnectionApprovalRequest request, 
            NetworkManager.ConnectionApprovalResponse response
        )
        {
            Debug.Log("CallApprovalCheck");
            
            if (networkManager.ConnectedClientsIds.Count >= maxPlayers)
            {
                response.Approved = false;
                response.Reason   = "Room is full.";
                response.Pending  = false;
                return;
            }
            
            response.Approved = true;
            response.CreatePlayerObject = true;
        }

        private void CallClientConnectedCallback(ulong clientId)
        {
            Debug.Log($"CallClientConnectedCallback {clientId}");
        }
    }
}
