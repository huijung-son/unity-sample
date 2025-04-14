using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void PlayertriggerDelegate(Collider other);
    public static event PlayertriggerDelegate playerTriggerEnter;
    public static event PlayertriggerDelegate playerTriggerExit;
    
    private float speed = 15f;
    
    public void Moving()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(h, 0, v);
        transform.Translate(speed * Time.deltaTime * movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        playerTriggerEnter?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        playerTriggerExit?.Invoke(other);
    }
}
