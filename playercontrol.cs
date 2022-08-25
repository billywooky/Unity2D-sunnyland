using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playercontrol : MonoBehaviour
{
    private  Rigidbody2D rb;
    private  Animator anim;
    public Collider2D coll;

    public float speed;
    public float jumpforce;

    public Transform groundcheck;
    
    public LayerMask ground;

    public bool isGround, isJump;
    bool jumpPressed;
    int jumpCount;

    private float horizontalMove;
    public int cherry;

    public Text cherrynumber;

    public bool isHurt;


    // Start is called before the first frame update

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input .GetButtonDown ("Jump")&&jumpCount>0)
        {
            jumpPressed = true;
        }
    }
    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundcheck.position, 0.1f, ground);
        if (!isHurt)
        {
           
            GroundMovement();
            jump();
        }

       
       
        SwitchAnim();
    }
    //角色移动
    void GroundMovement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");//只返回-1，0，1
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
    }

    void jump()
    {
        if(isGround )
        {
            jumpCount = 2;
            isJump = false;
        }
        if(jumpPressed &&isGround )
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            jumpCount--;
            jumpPressed = false;
        }
        else if(jumpPressed &&jumpCount >0&&isJump )
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            jumpCount--;
            jumpPressed = false;
        }
    }
  
    void SwitchAnim()
    {
        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));


        if (isHurt)
        {
            anim.SetBool("hurt", true);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
                isHurt = false;
            }


        }
        else if (isGround)
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);

        }

        else if (!isGround && rb.velocity.y > 0)
        {
            anim.SetBool("jumping", true);

        }
        else if (rb.velocity.y < 0)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);

        }
        
        




    }
    //收集道具
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision .tag =="collection")
        {
            Destroy(collision.gameObject);
            cherry += 1;
            cherrynumber.text = cherry.ToString();
        }
    }


    //消灭敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "enemy")
        {
            if (anim .GetBool ("falling"))
            {
                Destroy(collision.gameObject);
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                anim.SetBool("jumping", true);
            }else if(transform .position .x <collision .gameObject .transform .position .x)
            {
                rb.velocity = new Vector2(-5, rb.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x >collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(5, rb.velocity.y);
                isHurt = true;
            }
        }
    }
}
