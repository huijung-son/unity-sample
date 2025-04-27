using System.Collections;
using TMPro;
using UnityEngine;

namespace SonGame
{
    public class GameManager : MonoBehaviour
    {
        private Player player;
        private WeaponsProcess bullet;
        private TextMeshProUGUI text;
        
        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            bullet = Resources.Load<WeaponsProcess>("Prefabs/Bullet");
            text = GameObject.Find("Canvas").GetComponentInChildren<TextMeshProUGUI>();
            text.text = WeaponsProcess.process.ToString();
        }

        private void Start()
        {
            StartCoroutine(StartShootCoroutine());
        }

        private IEnumerator StartShootCoroutine()
        {
            while (true)
            {
                Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dir.Normalize();
                dir.z = 0;
                bullet.dir = dir;
                WeaponsProcess clone = Instantiate(bullet, player.transform.position, Quaternion.identity);
                Destroy(clone.gameObject, 5f);
                if (WeaponsProcess.process == WeaponsProcess.Process.Shotgun)
                {
                    float th = 10f * Mathf.Deg2Rad;
                    bullet.dir = new Vector3(
                        dir.x * Mathf.Cos(th) - dir.y * Mathf.Sin(th), 
                        dir.x * Mathf.Sin(th) + dir.y * Mathf.Cos(th), 
                        0f);
                    WeaponsProcess clone2 = Instantiate(bullet, player.transform.position, Quaternion.identity);
                    Destroy(clone2.gameObject, 5f);
                    
                    bullet.dir = new Vector3(
                        dir.x * Mathf.Cos(-th) - dir.y * Mathf.Sin(-th), 
                        dir.x * Mathf.Sin(-th) + dir.y * Mathf.Cos(-th), 
                        0f);
                    WeaponsProcess clone3 = Instantiate(bullet, player.transform.position, Quaternion.identity);         
                    Destroy(clone3.gameObject, 5f);
                }
                yield return clone.wait;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                WeaponsProcess.process += 1;
                text.text = WeaponsProcess.process.ToString();
            }
        }
    }
}
