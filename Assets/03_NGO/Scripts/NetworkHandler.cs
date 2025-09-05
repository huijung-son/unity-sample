using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

namespace Son
{
    public class NetworkHandler : MonoBehaviour
    {
        [SerializeField] private int maxPlayers = 4;
        
        private NetworkManager networkManager;
        private UnityTransport utp;
        private UdpClient udp;
        private IPEndPoint remoteAddress;
        
        private void Awake()
        {
            networkManager = GetComponent<NetworkManager>();
            utp = networkManager.GetComponent<UnityTransport>();
            udp = new UdpClient(utp.ConnectionData.Port);
            remoteAddress = new IPEndPoint(IPAddress.Any, utp.ConnectionData.Port);
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
            
            udp.Close();
        }

        private void Start()
        {
            byte[] sendBytes = Encoding.UTF8.GetBytes("son");
            udp.Send(
                sendBytes,
                sendBytes.Length,
                utp.ConnectionData.Address, 
                utp.ConnectionData.Port
                );
            
            byte[] data = udp.Receive(ref remoteAddress);
            Debug.Log($"Received {remoteAddress.Address} {remoteAddress.Port} {Encoding.UTF8.GetString(data)}");
            // StartCoroutine(UdpListening());
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
