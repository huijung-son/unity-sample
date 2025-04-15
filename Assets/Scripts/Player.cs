using UnityEngine;

public class Player : MonoBehaviour
{
    // 충돌 트리거 대리자
    public delegate void PlayerTriggerDelegate(Collider other);
    // 충돌 진입 이벤트 대리자
    public static event PlayerTriggerDelegate PlayerTriggerEnter;
    // 충돌 해제 이벤트 대리자
    public static event PlayerTriggerDelegate PlayerTriggerExit;
    // 이동속도
    private readonly float _speed = 15f;
    // 이동 목적지
    private Vector3 _moveDest = Vector3.zero;
    // 계속 이동할 조건
    private bool _moveProgress;
    
    // 키보드 이동 기능
    public void Moving()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(h, 0, v);
        transform.position = transform.position + (_speed * Time.deltaTime * movement);
    }
    
    // 마우스 이동 기능
    public void MovingWithMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo))
            {
                if (hitinfo.transform.CompareTag("Player")) return;
                _moveDest = hitinfo.point;
                _moveProgress = true;
            }
        }
        if (_moveProgress)
        {
            Vector3 moveDir = _moveDest - transform.position;
            moveDir.y = 0f;
            moveDir.Normalize();
            transform.position += (_speed * Time.deltaTime * moveDir);
            _moveProgress = (transform.position - _moveDest).magnitude > 0.05f;
        }
    }

    // 플레이어와 충돌시 콜백
    private void OnTriggerEnter(Collider other)
    {
        PlayerTriggerEnter?.Invoke(other);
    }

    // 플레이어와 충돌 해제시 콜백
    private void OnTriggerExit(Collider other)
    {
        PlayerTriggerExit?.Invoke(other);
    }
    
    // 마우스 포인터 방향으로 회전
    public void LookAtMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = transform.position.z;
        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 dir = mousePoint - transform.position;
        float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, -angle + 90f, 0f);
    }
}
