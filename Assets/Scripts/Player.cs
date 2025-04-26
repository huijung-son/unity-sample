using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 movement = Vector3.zero;

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            movement = new Vector3(h, v, 0f);
            transform.Translate(2f * Time.deltaTime * movement);
        }
    }
}
