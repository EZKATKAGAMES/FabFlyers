﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingGround : MonoBehaviour
{
    public float scrollSpeed = -1.5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(scrollSpeed * Time.deltaTime, 0));
    }
}
