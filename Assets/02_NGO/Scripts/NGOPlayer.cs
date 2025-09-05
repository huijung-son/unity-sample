using Unity.Netcode;
using UnityEngine;

public class NGOPlayer : NetworkBehaviour
{
    private void FixedUpdate()
    {
        if (IsOwner)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            if (horizontalInput != 0 || verticalInput != 0)
            {
                MoveServerRpc(horizontalInput, verticalInput);
            }
        }
    }

    [ServerRpc]
    private void MoveServerRpc(float horizontalInput, float verticalInput)
    {
        transform.position += new Vector3(horizontalInput, verticalInput, 0) * (Time.fixedDeltaTime * 4f);
    }
}
