using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_frog : MonoBehaviour
{
    private Rigidbody2D rb;

    public Transform leftpoint, rightponit;
    private bool Faceleft = true;
    public float speed;

    private float leftx, rightx;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightponit.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightponit.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
    }


    void Movement()
    {
        if (Faceleft)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if(transform .position .x<leftx )
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
                Faceleft = false;
            }
        }else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y );
            if (transform.position.x > rightx )
            {
                transform.localScale = new Vector3(transform.localScale.x, 1, 1);
                Faceleft = true;
            }

        }
    }
    

}
