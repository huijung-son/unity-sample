using System;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public delegate void MonsterTriggerDelegate(Collider other, Monster monster);
    public static event MonsterTriggerDelegate MonsterTriggerEnter;
    
    public Player TargetPlayer { get; set; }
    
    private float _moveSpeed = 3f;
    private float _hp = 100f;

    private void Update()
    {
        Vector3 dir = TargetPlayer.transform.position - transform.position;
        dir.Normalize();
        transform.position += (_moveSpeed * Time.deltaTime * dir);
        float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        transform.rotation =
            Quaternion.Euler(0f, -angle + 90f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        MonsterTriggerEnter?.Invoke(other, this);
    }
}
