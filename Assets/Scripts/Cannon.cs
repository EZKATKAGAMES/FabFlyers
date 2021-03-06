﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script will initialize the gameplay.
// It will allow you to aim & fire the cannon.

public class Cannon : MonoBehaviour
{
    Quaternion rotation;
    float width;
    float height;
    float speed = 5f;

    private Vector2 fireDirection;
   

    private void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;

        rotation = Quaternion.identity;

        
    }

    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       

        if(GameManager.GM.gameStateStarted == false)
        {
            // Cannon Look towards mouse position.
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

            // Clamp Z rotation between +90 and +20

            fireDirection = direction;
        }

       
        // Handle screen touches.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Aim cannon towards touchpoint.
            if (touch.phase == TouchPhase.Moved)
            {
                // AIM is to match cannon Z rotation with touch screen Y position

            }

        }
    }
}
