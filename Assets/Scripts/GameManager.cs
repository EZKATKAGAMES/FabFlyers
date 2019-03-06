using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM = null;
    public CinemachineVirtualCamera vcam;
    public bool gameStateStarted = false;

    public float stamina = 100;


    private void Awake()
    {
        if(GM == null)
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
    }

    
    void Update()
    {
        CameraControls();

        if(gameStateStarted == true)
        {
            ReduceStamina();
        }
        
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
            vcam.m_Lens.OrthographicSize = (1 - 0.015f) * vcam.m_Lens.OrthographicSize + 0.015f * 35;

        }


    }
}
