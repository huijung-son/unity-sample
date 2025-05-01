using UnityEngine;

public class CustomMainCamera : MonoBehaviour
{
    private GameObject player;
    private float transX;
    private float transY;
    private float transZ;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transX = transform.position.x;
        transY = transform.position.y;
        transZ = transform.position.z;
    }

    private void Update()
    {
        transform.position = new Vector3(
            player.transform.position.x + transX, 
            player.transform.position.y + transY, 
            player.transform.position.z + transZ);         
    }
}
