using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player _scriptPlayer;
    private GameObject _prefabMachine;
    private GameObject _gameObjectMachineManager;
    
    private void OnEnable()
    {
        Player.OnPlayerTriggerEnterEvent += HandlePlayerTriggerEnter;
        Player.OnPlayerTriggerExitEvent += HandlePlayerTriggerExit;
    }

    private void OnDisable()
    {
        Player.OnPlayerTriggerEnterEvent -= HandlePlayerTriggerEnter;
        Player.OnPlayerTriggerExitEvent -= HandlePlayerTriggerExit;
    }
    
    private void Awake()
    {
        // 플레이어 초기세팅
        this.InitPlayer();
        this.InitMachineManager();
        this.InitMachine();
    }

    private void Update()
    {
        // 플레이어의 무빙
        this._scriptPlayer.Moving();
    }

    // Awake
    private void InitPlayer()
    {
        GameObject gameObjectplayer = GameObject.Find("Player");
        gameObjectplayer.AddComponent<Player>();
        this._scriptPlayer = gameObjectplayer.GetComponent<Player>();
    }

    private void InitMachineManager()
    {
        _gameObjectMachineManager = GameObject.Find("MachineManager");
    }

    private void InitMachine()
    {
        this._prefabMachine = Resources.Load<GameObject>("Prefabs\\PMachine");
        for (int i = 0; i < 5; ++i)
        {
            Quaternion q = new Quaternion(0f, 0f, 0f, 0f);
            Vector3 pos = this._prefabMachine.transform.position;
            pos.x = Random.Range(-15f, 15f);
            pos.z = Random.Range(-15f, 15f);
            GameObject cloneMachine = Instantiate(this._prefabMachine, pos, q);
            cloneMachine.AddComponent<Machine>();
            cloneMachine.transform.SetParent(_gameObjectMachineManager.transform);
        }
    }
    
    // Player
    private void HandlePlayerTriggerEnter(GameObject player, Collider other)
    {
        if (other.CompareTag("Machine"))
        {
            other.GetComponent<Machine>().Interaction();
        }
    }

    private void HandlePlayerTriggerExit(GameObject player, Collider other)
    {
        if (other.CompareTag("Machine"))
        {
            other.GetComponent<Machine>().Interaction();
        }
    }
}
