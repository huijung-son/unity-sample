using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDefault : MonoBehaviour
{
    private GameObject bullet = null;
    private WaitForSeconds wait = new WaitForSeconds(10f);
    private GameObject initBullet1 = null;
    private GameObject initBullet2 = null;
    private GameObject initBullet3 = null;
    private GameObject initBullet4 = null;
    private Process2 p2 = null;
    private Process3 p3 = null;
    private Process4 p4 = null;
    private List<GameObject> bullets = new List<GameObject>();
    
    private void Awake()
    {
        bullet = Resources.Load<GameObject>("Prefabs/Bullet");
        initBullet1 = Instantiate(bullet, transform.position, Quaternion.identity);
        initBullet1.AddComponent<Process1>();
        bullets.Add(initBullet1);
        
        initBullet2 = Instantiate(bullet, transform.position, Quaternion.identity);
        p2 = initBullet2.AddComponent<Process2>();
        bullets.Add(initBullet2);
        
        initBullet3 = Instantiate(bullet, transform.position, Quaternion.identity);
        p3 = initBullet3.AddComponent<Process3>();
        bullets.Add(initBullet3);
        
        initBullet4 = Instantiate(bullet, transform.position, Quaternion.identity);
        p4 = initBullet4.AddComponent<Process4>();
        bullets.Add(initBullet4);
    }

    private void Start()
    {
        StartCoroutine(BulletInit());
    }

    private IEnumerator BulletInit()
    {
        while (true)
        {
            initBullet1.transform.position = transform.position;
            if (p2 != null)
            {
                p2.pos = transform.position;
            }
            if (p3 != null)
            {
                p3.pos = transform.position;
            }
            yield return wait;
        }
    }

    private class Process1 : MonoBehaviour
    {
        private void Update()
        {
            transform.position += Time.deltaTime * 4f * (Vector3.right + Vector3.down);
        }
    }
    
    private class Process2 : MonoBehaviour
    {
        public Vector3 pos;

        private void Awake()
        {
            pos = transform.position;
        }

        private void Update()
        {
            pos += 4f * Time.deltaTime * (Vector3.forward);
            transform.position = pos + new Vector3(Mathf.Sin(Time.time * 5f) * 20f, 0f, 0f);
        }
    }
    
    private class Process3 : MonoBehaviour
    {
        public Vector3 pos;

        private void Awake()
        {
            pos = transform.position;
        }

        private void Update()
        {
            // 앞으로 나가는 속도
            pos += 3f * Time.deltaTime * (Vector3.left + Vector3.down);
            // 원래 갈려고 하는 방향                             운동속도  진폭
            Vector3 dir = new Vector3(Mathf.Sin(Time.time * 10f) * 2f, 0f, 0f);
            //dir.Normalize();
            float angle = -Mathf.PI * 0.25f;
            transform.position = pos + new Vector3(
                dir.x * Mathf.Cos(angle) - dir.y * Mathf.Sin(angle),
                dir.x * Mathf.Sin(angle) + dir.y * Mathf.Cos(angle),
                0f
                );
        }
    }

    private class Process4 : MonoBehaviour
    {
        private Vector3 target;

        private void Awake()
        {
            target = transform.position;
        }

        private void Update()
        {
            float angle = (2 * Mathf.PI) / (Time.deltaTime * 0.02f);
            transform.position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * 5f;
        }
    }
}

