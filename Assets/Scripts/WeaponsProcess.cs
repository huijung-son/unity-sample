using UnityEngine;

public class WeaponsProcess : MonoBehaviour
{
    public enum Process
    {
        Default, Double, Twist, Shotgun, Circle
    }
    public static Process process = Process.Twist;
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
                speed = 3f;
                wait = new WaitForSeconds(0.5f);
                break;
            case Process.Twist:
                updateProcess = TwistShoot;
                speed = 4f;
                wait = new WaitForSeconds(0.3f);
                break;
            case Process.Shotgun:
                updateProcess = ShotgunShoot;
                speed = 4f;
                wait = new WaitForSeconds(0.3f);
                break;
            case Process.Circle:
                updateProcess = CircleShoot;
                speed = 4f;
                wait = new WaitForSeconds(0.3f);
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
        Vector3 pos = dir + new Vector3(Mathf.Sin(Time.time), 0f, 0f);
        transform.position += speed * Time.deltaTime * pos.normalized;
    }

    private void ShotgunShoot()
    {
        transform.position += speed * Time.deltaTime * dir;
    }

    private void CircleShoot()
    {
        transform.position += speed * Time.deltaTime * dir;
    }
}
