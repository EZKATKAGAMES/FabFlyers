using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float upForce = 550f;
    public float horizontalForce = 800f;

    private bool isDead = false;
    private Rigidbody2D rigid;
    private Animator anim;
    
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        
    }

    void Update()
    {
        // when we are alive and have the stamina, allow flapping.
        if (isDead == false && GameManager.GM.stamina >= 20)
        {
            Flap();
        }
    }


    void Flap()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rigid.velocity = Vector2.zero;
            rigid.AddForce(new Vector2(0, upForce));
            anim.SetTrigger("Flap");

            GameManager.GM.stamina -= 20;
        }
    }

    public void Die()
    {
        rigid.velocity = Vector2.zero;
        isDead = true;
        anim.SetTrigger("Die");
    }
}
