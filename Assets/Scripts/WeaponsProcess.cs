using UnityEngine;

public class WeaponsProcess : MonoBehaviour
{
    public WaitForSeconds wait = new WaitForSeconds(0.5f);
    public enum Process
    {
        Default, Double
    }
    public Process process = Process.Default;
    private delegate void UpdateProcessDelegate();
    private UpdateProcessDelegate updateProcess;
    
    private void Awake()
    {
        switch (process)
        {
            case Process.Default:
                updateProcess = DefaultShoot;
                break;
            case Process.Double:
                updateProcess = DoubleShoot;
                break;
        }
    }

    private void Update()
    {
        updateProcess?.Invoke();
    }

    private void DefaultShoot()
    {
        transform.position += 2f * Time.deltaTime * transform.up;
    }

    private void DoubleShoot()
    {
        transform.position += 2f * Time.deltaTime * transform.right;
    }
}
