using Unity.Netcode;
using UnityEngine;

namespace Son
{
    public class NetworkHandler : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private int maxPlayers = 4;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            Debug.Log("Start");
            NetworkManager.Singleton.ConnectionApprovalCallback += CallApprovalCheck;
            NetworkManager.Singleton.OnClientConnectedCallback += CallClientConnectedCallback;
        }

        private void OnDestroy()
        {
            Debug.Log("OnDestroy");
            NetworkManager.Singleton.ConnectionApprovalCallback -= CallApprovalCheck;
            NetworkManager.Singleton.OnClientConnectedCallback -= CallClientConnectedCallback;
        }
        
        private void CallApprovalCheck(
            NetworkManager.ConnectionApprovalRequest request, 
            NetworkManager.ConnectionApprovalResponse response
        )
        {
            Debug.Log("CallApprovalCheck");
            if (NetworkManager.Singleton.ConnectedClientsIds.Count >= maxPlayers)
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
            Debug.Log("CallClientConnectedCallback");
        }
    }
}

