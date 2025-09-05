using UnityEngine;

namespace Son
{
    public class GameSystemManager : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}

