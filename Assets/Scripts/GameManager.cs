using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    // Important!!!
    public static GameManager GM = null;
    public ScrollingGround[] scrollingGround;
    public bool gameStateStarted = false;

    // Camera
    [Header("Camera")]
    public bool cameraShake = true;
    public bool cameraBob = true;
    public CinemachineVirtualCamera vcam;
    public float finalZoom; // zoom level we LERP towards.
    private float timer; // Dutch Speed
    private float bobAmount;

    #region CameraFramingTransposerVariables

    float screenY = 0.5f; // DEFUALT
    float screenX = 0.3f; 

    #endregion

    // Gameplay
    [Header("Gameplay")]
    public float stamina = 100;
    public OrcaEnemyPool orcaEnemyPool;
    public Transform parentTransform; // Ensure correct instantiation within heirarchy
   
    private Transform fireLocation;
    private Vector3 fireLocationVector;


    private void Awake()
    {
        parentTransform = GameObject.Find("[Group]Ground").transform;

        if (GM == null)
        {
            GM = this;
            if(GM != this)
            {
                Instantiate(Resources.Load("Prefabs/GM") as GameObject);   
            }
 
        }
    }

    void Start()
    {
        // Setup camera reference, set follow target. (required for editing certain values)
        vcam = GameObject.FindGameObjectWithTag("Vcam").GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = GameObject.FindGameObjectWithTag("Cannon").transform;

        scrollingGround = FindObjectsOfType<ScrollingGround>();
        orcaEnemyPool = FindObjectOfType<OrcaEnemyPool>();
        orcaEnemyPool.enabled = false;
    }

    private void FixedUpdate()
    {
        CameraControls();
    }

    void Update()
    {
        #region GameStateFALSE
        // Functions in update that occur when game has not begun.
        // Disable scrolling.
        if (gameStateStarted == false)
        {
            scrollingGround[0].enabled = false;
            scrollingGround[1].enabled = false;
            scrollingGround[2].enabled = false;

            // Fire the cannon when tapped.
            if (Input.GetMouseButtonDown(0))
            {
                if (gameStateStarted == false)
                {
                    Fire();
                }
            }
        }

        #endregion

        #region GameStateTRUE
        // Functions in update that occur when game has begun.
    
        if (gameStateStarted == true)
        {
            ReduceStamina();
            orcaEnemyPool.enabled = true;
        }

        #endregion

        // Aiming with the updating fireLocation (parented to cannon).
        fireLocation = GameObject.Find("FirePoint").transform;
        fireLocationVector = fireLocation.position;

    }


    public void Fire()
    {
        Instantiate(Resources.Load("Sprites/Puffin Model/puffin01mid"), fireLocationVector, Quaternion.identity ,parentTransform);
        gameStateStarted = true;
        scrollingGround[0].enabled = true;
        scrollingGround[1].enabled = true;
        scrollingGround[2].enabled = true;
    }

    void ReduceStamina()
    {
        // Pasively reduce stamina in flight if no actions are occuring.
        stamina -= Time.deltaTime / 3;

        // reduce stamina upon each wing flap

        // slow stamina reduction in wind & glide
    }

    void CameraControls()
    {
        

        // Set Target to Bird.
        if (gameStateStarted == true)
        {
            // Set follow target.
            vcam.Follow = GameObject.FindGameObjectWithTag("Player").transform;
            // Lerp to desired orthographic size (zoom out)
            vcam.m_Lens.OrthographicSize = (1 - 0.015f) * vcam.m_Lens.OrthographicSize + 0.015f * finalZoom;
            vcam.m_Lens.Dutch = 0;
            vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX
            = (1 - 0.1f) * vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX + 0.1f * 0.5f;
            screenY = 0.5f;
        }
        
        // Camera settings when game has not begun.
        if(cameraBob && gameStateStarted == false)
        {
            // Camera bobbing
            bobAmount = Mathf.Lerp(0.49f, 0.51f, Mathf.PingPong(Time.time, 1));
            screenY = (1 - 0.01f) * screenY + 0.01f * bobAmount;
            vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = screenY;
            // SWAY = delay follow a moving target (ez way)

        }

        // Camera Shake
        if (cameraShake == true && gameStateStarted == false)
        {
            // Only shake when state is not playing.
            float randomNeg = Random.Range(-0.3f, -1.2f);
            float randomPos = Random.Range(0.3f, 1.2f);
            float speed = 1f;
            // Lerping the value we are lerping by. funni meem zxddd
            timer = Mathf.Lerp(randomNeg, randomPos, Mathf.PingPong(Time.time, speed));
            vcam.m_Lens.Dutch = (1 - 0.02f) * vcam.m_Lens.Dutch + 0.02f * timer;
        }

    }
}
