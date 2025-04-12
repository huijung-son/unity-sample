using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void TriggerEventHandler(GameObject player, Collider other);
    public static event TriggerEventHandler OnPlayerTriggerEnterEvent;
    public static event TriggerEventHandler OnPlayerTriggerExitEvent;
    
    private readonly float _speed = 10f;
    
    // 기능 : 움직인다
    public void Moving()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector3 moveDir = new Vector3(h, 0, v);
        this.transform.Translate( _speed * Time.deltaTime * moveDir);
    }
    
    // 이벤트 : 콜라이더 접근
    private void OnTriggerEnter(Collider other)
    {
        OnPlayerTriggerEnterEvent?.Invoke(gameObject, other);
    }

    // 이벤트 : 콜라이더 해제
    private void OnTriggerExit(Collider other)
    {
        OnPlayerTriggerExitEvent?.Invoke(gameObject, other);
    }
}
