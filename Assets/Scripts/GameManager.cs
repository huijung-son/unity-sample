using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Player player;
    private Machine nowHitMachineSC = null;
    private MenuCanvas menuCanvasSC = null;
    
    private void Awake()
    {
        // 플레이어 세팅
        GameObject gameObjectplayer = GameObject.Find("Player");
        gameObjectplayer.AddComponent<Player>();
        player = gameObjectplayer.GetComponent<Player>();
        
        // 자판기 세팅
        GameObject machinePrefab = Resources.Load<GameObject>("Prefabs\\PMachine");
        Vector3 machinePosition = machinePrefab.transform.position;
        machinePosition.x = 3f;
        machinePosition.z = 3f;
        Quaternion machineRotation = Quaternion.identity;
        GameObject cloneMachine = Instantiate(machinePrefab, machinePosition, machineRotation);
        cloneMachine.AddComponent<Machine>();
        
        // 메뉴 캔버스 세팅
        GameObject menuCanvas = GameObject.Find("MenuCanvas");
        menuCanvas.AddComponent<MenuCanvas>();
        menuCanvasSC = menuCanvas.GetComponent<MenuCanvas>();
        menuCanvasSC.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Player.playerTriggerEnter += PlayerTriggerEnterManager;
        Player.playerTriggerExit += PlayerTriggerExitManager;
    }

    private void OnDisable()
    {
        Player.playerTriggerEnter -= PlayerTriggerEnterManager;
        Player.playerTriggerExit -= PlayerTriggerExitManager;
    }

    private void Update()
    {
        player.Moving();
        OnMenu();
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

    private void OnMenu()
    {
        if (nowHitMachineSC != null && Input.GetKeyDown(KeyCode.E))
        {
            menuCanvasSC.gameObject.SetActive(true);
            Transform btntran = menuCanvasSC.transform.Find("Button");
            if (btntran != null)
            {
                GameObject btnGo = btntran.gameObject;
                Button btn = btnGo.GetComponent<Button>();
                btn.onClick.AddListener(() =>
                {
                    menuCanvasSC.gameObject.SetActive(false);
                });
            }
        }
    }
}
