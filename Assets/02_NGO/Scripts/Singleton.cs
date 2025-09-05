using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static object _lock = new object();
    private static bool _shuttingDown = false;

    public static T Instance
    {
        get
        {
            if (_shuttingDown) return null;

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<T>();

                    if (_instance == null)
                    {
                        var singletonObject = new GameObject(typeof(T).Name);
                        _instance = singletonObject.AddComponent<T>();
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return _instance;
            }
        }
    }

    protected virtual void OnApplicationQuit()
    {
        _shuttingDown = true;
    }

    protected virtual void OnDestroy()
    {
        _shuttingDown = true;
    }
}