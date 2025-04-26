using System.Collections;
using UnityEngine;

namespace SonGame
{
    public class GameManager : MonoBehaviour
    {
        private Player player;
        private WeaponsProcess bullet;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            bullet = Resources.Load<WeaponsProcess>("Prefabs/Bullet");
        }

        private void Start()
        {
            StartCoroutine(StartShootCoroutine());
        }

        private IEnumerator StartShootCoroutine()
        {
            while (true)
            {
                WeaponsProcess clone = Instantiate(bullet, player.transform.position, Quaternion.identity);
                Destroy(clone, 5f);
                yield return clone.wait;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                bullet.process += 1;
            }
        }
    }
}
