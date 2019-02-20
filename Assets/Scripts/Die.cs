using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            print(col.gameObject.tag);

            col.gameObject.GetComponent<Bird>().Die();
        }
    }
}
