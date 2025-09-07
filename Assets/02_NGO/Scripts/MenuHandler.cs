using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Son
{
    public class MenuHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmp;
        
        private UdpClient udp;
        private UnityTransport utp;
        private IPEndPoint address = new IPEndPoint(IPAddress.Any, 47777);
        private IPEndPoint sendEP;
        private float listeningTime = 0f;
        
        private void OnEnable()
        {
            if (NetworkManager.Singleton != null)
            {
                if (NetworkManager.Singleton.SceneManager != null && NetworkManager.Singleton.IsListening)
                {
                    NetworkManager.Singleton.SceneManager.OnSceneEvent += CallSceneEvent;
                }
            }
        }

        private void OnDisable()
        {
            if (NetworkManager.Singleton != null)
            {
                if (NetworkManager.Singleton.SceneManager != null && NetworkManager.Singleton.IsListening)
                {
                    NetworkManager.Singleton.SceneManager.OnSceneEvent -= CallSceneEvent;
                }
            }

            if (udp != null)
            {
                udp.Close();
                udp = null;
            }
        }
        
        private IPAddress GetSubnetBroadcast()
        {
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus != OperationalStatus.Up) continue;
                var ipProps = nic.GetIPProperties();
                foreach (var ua in ipProps.UnicastAddresses)
                {
                    if (ua.Address.AddressFamily != AddressFamily.InterNetwork) continue;
                    var ip = ua.Address.GetAddressBytes();
                    var mask = ua.IPv4Mask.GetAddressBytes();
                    var bc = new byte[4];
                    for (int i = 0; i < 4; i++) bc[i] = (byte)(ip[i] | ~mask[i]);
                    return new IPAddress(bc);
                }
            }
            return IPAddress.Broadcast; // 폴백
        }

        private void Start()
        {
            utp = NetworkManager.Singleton.GetComponent<UnityTransport>();
            
            udp = new UdpClient(47777);
            udp.EnableBroadcast = true; 
            udp.Client.Blocking = false;
            
            var bc = GetSubnetBroadcast();              // 예: 192.168.0.255
            sendEP = new IPEndPoint(bc, 47777);
        }

        private void LateUpdate()
        {
            listeningTime += Time.deltaTime;
            if (listeningTime > 3)
            {
                listeningTime = 0;
                
                if (!NetworkManager.Singleton.IsListening)
                {
                    Receive();
                }
                else if (NetworkManager.Singleton.IsHost)
                {
                    byte[] sendBytes = Encoding.UTF8.GetBytes("son");
                    udp.Send(
                        sendBytes,
                        sendBytes.Length,
                        sendEP
                    );
                }
            }
        }

        public void OnStartClient()
        {
            if (!NetworkManager.Singleton.IsListening && NetworkManager.Singleton.StartClient())
            {
                NetworkManager.Singleton.SceneManager.OnSceneEvent -= CallSceneEvent;
                NetworkManager.Singleton.SceneManager.OnSceneEvent += CallSceneEvent;
            }
        }

        public void OnStartHost()
        {
            if (!NetworkManager.Singleton.IsListening)
            {
                // string localIP = GetInternalIP();
                utp.SetConnectionData("127.0.0.1", utp.ConnectionData.Port, "192.168.0.5");
                
                if (NetworkManager.Singleton.StartHost())
                {
                    NetworkManager.Singleton.SceneManager.OnSceneEvent -= CallSceneEvent;
                    NetworkManager.Singleton.SceneManager.OnSceneEvent += CallSceneEvent;
                }
            }
        }

        public void OnNextScene(string sceneName)
        {
            NetworkManager.Singleton.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        
        private void CallSceneEvent(SceneEvent e)
        {
            if (!NetworkManager.Singleton.IsServer) return;
            
            Debug.Log($"CallSceneEvent {e.SceneEventType}");
        }
        
        private string GetInternalIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            
            throw new Exception("IPv4 주소를 찾을 수 없습니다.");
        }
        
        private void Receive()
        {
            try
            {
                byte[] bytes = udp.Receive(ref address);
                tmp.text = Encoding.UTF8.GetString(bytes);
                Debug.Log($"[Receive] Remote IpEndPoint : {address.ToString()} Size : {bytes.Length} byte");
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
                return;
            }
        }
    }
}

