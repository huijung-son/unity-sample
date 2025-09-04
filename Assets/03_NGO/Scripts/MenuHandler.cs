using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Son
{
    public class MenuHandler : MonoBehaviour
    {
        public void OnStartClient()
        {
            if (!NetworkManager.Singleton.IsListening && NetworkManager.Singleton.StartClient())
            {
                NetworkManager.Singleton.SceneManager.OnSceneEvent += CallSceneEvent;
            }
        }

        public void OnStartHost()
        {
            if (!NetworkManager.Singleton.IsListening && NetworkManager.Singleton.StartHost())
            {
                NetworkManager.Singleton.SceneManager.OnSceneEvent += CallSceneEvent;
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
    }
}

