using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void PlayertriggerDelegate(Collider other);
    public static event PlayertriggerDelegate playerTriggerEnter;
    public static event PlayertriggerDelegate playerTriggerExit;
    
    private float speed = 15f;
    private Vector3 moveDest = Vector3.zero;
    bool moveProgress = false;
    
    public void Moving()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(h, 0, v);
        transform.Translate(speed * Time.deltaTime * movement);
    }

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

                moveDest = hitinfo.point;
                
                Vector3 dir = moveDest - transform.position;
                float yawAngle = Mathf.Atan2(dir.z, dir.x);
                yawAngle *= Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, -yawAngle + 90f, 0f);
                moveProgress = true;
            }
        }
        MovingWithMouseProcess();
    }

    public void MovingWithMouseProcess()
    {
        if (moveProgress)
        {
            Vector3 moveDir = moveDest - transform.position;
            moveDir.y = 0f;
            moveDir.Normalize();
            transform.position = transform.position + (speed * Time.deltaTime * moveDir);
            moveProgress = (transform.position - moveDest).magnitude > 0.05f;
        }
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
