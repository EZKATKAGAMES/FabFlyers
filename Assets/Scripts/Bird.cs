using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float upForce = 400f;

    private bool isDead = false;
    private Rigidbody2D rigid;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
        {
            Flap();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            rigid.velocity = Vector2.zero;
            isDead = true;
            anim.SetTrigger("Die");
        }
    }

    void Flap()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rigid.velocity = Vector2.zero;
            rigid.AddForce(new Vector2(0, upForce));
            anim.SetTrigger("Flap");
        }
    }
}
