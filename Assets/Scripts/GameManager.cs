using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM = null;
    public CinemachineVirtualCamera vcam;
    public bool gameStateStarted = false;
    public Transform parentTransform;
    public ScrollingGround[] scrollingGround;
    public OrcaEnemyPool orcaEnemyPool;

    public float stamina = 100;
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
        vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
       
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

        if(gameStateStarted == false)
        {
            scrollingGround[0].enabled = false;
            scrollingGround[1].enabled = false;
            scrollingGround[2].enabled = false;
        }


        fireLocation = GameObject.Find("FirePoint").transform;
        fireLocationVector = fireLocation.position;

        if (gameStateStarted == true)
        {
            ReduceStamina();
            orcaEnemyPool.enabled = true;
        }

        // Fire the cannon when tapped.
        if (Input.GetMouseButtonDown(0))
        {
            if (gameStateStarted == false)
            {
                Fire();
            }
        }
    }


    public void Fire()
    {
        Instantiate(Resources.Load("Prefabs/Bird"), fireLocationVector, Quaternion.identity ,parentTransform);
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
        if(gameStateStarted == true)
        {
            vcam.Follow = GameObject.FindGameObjectWithTag("Player").transform;
            // Lerp to desired orthographic size (zoom out)
            vcam.m_Lens.OrthographicSize = (1 - 0.015f) * vcam.m_Lens.OrthographicSize + 0.015f * 20;

        }


    }
}
