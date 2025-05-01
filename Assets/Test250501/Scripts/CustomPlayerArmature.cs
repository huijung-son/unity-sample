using UnityEngine;

public class CustomPlayerArmature : MonoBehaviour
{
    private Animator animator;
    public Transform target;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Attack", true);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetLayerWeight(1, 1f);
        }
    }

    public void StopAttack()
    {
        animator.SetBool("Attack", false);
    }
}
