using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script will initialize the gameplay.
// It will allow you to aim & fire the cannon.

public class Cannon : MonoBehaviour
{
    Quaternion rotation;
    float width;
    float height;

    private void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;

        rotation = Quaternion.identity;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      // Handle screen touches.
      if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

             // Aim cannon towards touchpoint.
             if(touch.phase == TouchPhase.Moved)
            {
                // Probably incorrect, needs testing.
                // AIM is to match cannon Z rotation with touch screen Y position.
                Vector2 pos = touch.position;
                pos.x = (pos.x - width) / width;
                pos.y = (pos.y - height) / height;
                rotation = Quaternion.Euler(0, 0, pos.y);

                transform.rotation = rotation;

            }

        }
    }
}
