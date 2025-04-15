using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 플레이어 스크립트 컴포넌트
    private Player _player;
    // 카메라 세팅시 오프셋
    private Vector3 _cameraOffset = Vector3.zero;
    // 메인 카메라 오브젝트
    private GameObject _mainCamera;
    // 몬스터 스크립트 컴포넌트
    private GameObject _monsterPrefab;
    // 몬스터 스폰 거리
    private float _spawnDist = 40f;
    // 스폰 시간
    private float _spawnTimer = 0f;
    // 총알
    private GameObject _bulletPrefab;
    
    private void OnEnable()
    {
        Player.PlayerTriggerEnter += PlayerTriggerEnterManager;
        Player.PlayerTriggerExit += PlayerTriggerExitManager;
        
        Monster.MonsterTriggerEnter += MonsterTriggerEnterManager;
    }
    
    private void Awake()
    {
        // 플레이어 세팅
        GameObject gameObjectplayer = GameObject.Find("Player");
        gameObjectplayer.AddComponent<Player>();
        _player = gameObjectplayer.GetComponent<Player>();
        
        // 카메라 세팅
        _mainCamera = GameObject.Find("Main Camera");
        
        // 몬스터 프리팹
        _monsterPrefab = Resources.Load<GameObject>("Prefabs/PMonster");
        
        // 총알 프리팹
        _bulletPrefab = Resources.Load<GameObject>("Prefabs/PBullet");
    }
    
    private void Start()
    {
        _cameraOffset = _mainCamera.transform.position - _player.transform.position;
    }

    private void Update()
    {
        _player.Moving();
        //player.MovingWithMouse(); 주석 풀면 버그 있음
        _player.LookAtMouse();
        FollowCamera();
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= 1f)
        {
            SpawnMonster();
            _spawnTimer = 0f;
        }

        ShootCoroutine();
    }
    
    private void OnDisable()
    {
        Player.PlayerTriggerEnter -= PlayerTriggerEnterManager;
        Player.PlayerTriggerExit -= PlayerTriggerExitManager;
        
        Monster.MonsterTriggerEnter -= MonsterTriggerEnterManager;
    }

    private void PlayerTriggerEnterManager(Collider other)
    {
        
    }
    
    private void PlayerTriggerExitManager(Collider other)
    {
        
    }

    private void MonsterTriggerEnterManager(Collider other, Monster monster)
    {
        Destroy(other.gameObject);
    }

    private void FollowCamera()
    {
        _mainCamera.transform.position = _player.transform.position + _cameraOffset;
    }

    private void SpawnMonster()
    {
        GameObject monster = Instantiate(_monsterPrefab);
        float theta = Random.Range(0f, 360f);
        Vector3 pos = new Vector3(Mathf.Cos(theta), 0f, Mathf.Sin(theta)) * _spawnDist;
        monster.transform.position = pos;
        monster.AddComponent<Monster>();
        Monster monsterScript = monster.GetComponent<Monster>();
        monsterScript.TargetPlayer = _player;
    }
    
    // 기본 평타 무기 온
    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = transform.position.z;
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 dir = mousePoint - _player.transform.position;
            dir.Normalize();
            
            GameObject bulletPrefab = Instantiate(_bulletPrefab);
            bulletPrefab.transform.position = _player.transform.position;
            yield return new WaitForSeconds(1f);
        }
    }
}
