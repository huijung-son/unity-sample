using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Player player;
    private Machine nowHitMachineSC = null;
    private Vector3 cameraOffset = Vector3.zero;
    private GameObject mainCamera = null;
    
    private void OnEnable()
    {
        Player.playerTriggerEnter += PlayerTriggerEnterManager;
        Player.playerTriggerExit += PlayerTriggerExitManager;
    }
    
    private void Awake()
    {
        // 플레이어 세팅
        GameObject gameObjectplayer = GameObject.Find("Player");
        gameObjectplayer.AddComponent<Player>();
        player = gameObjectplayer.GetComponent<Player>();
        
        // 카메라 세팅
        mainCamera = GameObject.Find("Main Camera");
        
        // 자판기 세팅
        GameObject machinePrefab = Resources.Load<GameObject>("Prefabs\\PMachine");
        Vector3 machinePosition = machinePrefab.transform.position;
        machinePosition.x = 3f;
        machinePosition.z = 3f;
        Quaternion machineRotation = Quaternion.identity;
        GameObject cloneMachine = Instantiate(machinePrefab, machinePosition, machineRotation);
        cloneMachine.AddComponent<Machine>();
    }
    
    private void Start()
    {
        cameraOffset = mainCamera.transform.position - player.transform.position;
    }


    private void Update()
    {
        player.Moving();
        player.MovingWithMouse();
        FollowCamera();
        OnMenu();
    }
    
    private void OnDisable()
    {
        Player.playerTriggerEnter -= PlayerTriggerEnterManager;
        Player.playerTriggerExit -= PlayerTriggerExitManager;
    }

    private void PlayerTriggerEnterManager(Collider other)
    {
        if (other.CompareTag("Machine"))
        {
            nowHitMachineSC = other.GetComponent<Machine>();
        }
    }
    
    private void PlayerTriggerExitManager(Collider other)
    {
        if (other.CompareTag("Machine"))
        {
            nowHitMachineSC = null;
        }
    }

    private void FollowCamera()
    {
        mainCamera.transform.position = player.transform.position + cameraOffset;
    }

    private void OnMenu()
    {
        if (nowHitMachineSC != null && Input.GetKeyDown(KeyCode.E))
        {
            
        }
    }
}
