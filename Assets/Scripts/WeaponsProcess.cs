using UnityEngine;

public class WeaponsProcess : MonoBehaviour
{
    public enum Process
    {
        Default, Double
    }
    public static Process process = Process.Default;
    private delegate void UpdateProcessDelegate();
    private UpdateProcessDelegate updateProcess;
    // 총알스펙
    public WaitForSeconds wait;
    public Vector3 dir = Vector3.up;
    private float speed = 2f;
    
    private void Awake()
    {
        switch (process)
        {
            case Process.Default:
                updateProcess = DefaultShoot;
                speed = 2f;
                wait = new WaitForSeconds(1f);
                break;
            case Process.Double:
                updateProcess = FastShoot;
                speed = 4f;
                wait = new WaitForSeconds(0.2f);
                break;
            default:
                process = Process.Default;
                updateProcess = DefaultShoot;
                speed = 2f;
                wait = new WaitForSeconds(1f);
                break;
        }
    }

    private void Update()
    {
        updateProcess?.Invoke();
    }

    private void DefaultShoot()
    {
        transform.position += speed * Time.deltaTime * dir;
    }

    private void FastShoot()
    {
        transform.position += speed * Time.deltaTime * dir;
    }

    private void TwistShoot()
    {
        
    }
}
