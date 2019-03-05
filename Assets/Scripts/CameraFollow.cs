using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make sure camera follows target once game starts.
 
public class CameraFollow : MonoBehaviour
{
    // Follow variables    


    void Start()
    {
        
    }

   
    void Update()
    {
        if(GameManager.GM.gameStateStarted == false)
        {
            // SET TARGET TO CANNON
        }
        else if (GameManager.GM.gameStateStarted == true)
        {
            // SET TARGET TO BIRD
        }
    }
}
