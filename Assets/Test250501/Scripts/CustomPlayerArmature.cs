using UnityEngine;

public class CustomPlayerArmature : MonoBehaviour
{
    private Animator animator;
    private GameObject target = null;
    private bool lookAt = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("TargetOther");
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Attack", true);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            lookAt = !lookAt;
            animator.SetLayerWeight(1, lookAt ? 1f : 0f);
        }
        
        if (target != null && lookAt)
        {
            transform.LookAt(target.transform);
        }
    }

    public void StopAttack()
    {
        animator.SetBool("Attack", false);
    }
}
