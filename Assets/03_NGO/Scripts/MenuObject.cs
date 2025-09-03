using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Son
{
    public class MenuObject : MonoBehaviour
    {
        public void OnStartClient()
        {
            if (NetworkManager.Singleton.StartClient())
            {
                NetworkManager.Singleton.SceneManager.OnSceneEvent += CallSceneEvent;
            }
        }

        public void OnStartHost()
        {
            if (NetworkManager.Singleton.StartHost())
            {
                NetworkManager.Singleton.SceneManager.OnSceneEvent += CallSceneEvent;
            }
        }

        public void OnNextScene()
        {
            NetworkManager.Singleton.SceneManager.LoadScene("NGONextScene", LoadSceneMode.Single);
        }
        
        private void CallSceneEvent(SceneEvent e)
        {
            if (!NetworkManager.Singleton.IsServer) return;
            
            Debug.Log("CallSceneEvent");
            
            if (e.SceneEventType == SceneEventType.SynchronizeComplete)
            {
                Debug.Log("e.SceneEventType == SceneEventType.SynchronizeComplete");
            }
        }
    }
}

