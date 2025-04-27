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
                dir.z = 0;
                bullet.dir = dir.normalized;
                WeaponsProcess clone = Instantiate(bullet, player.transform.position, Quaternion.identity);
                Destroy(clone.gameObject, 5f);
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
