using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private LayerMask groundLayers;
    [SerializeField]
    private float jumpCheckOffset = 0.1f;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject visuals;
    [SerializeField]
    private AudioSource JumpSFX;

    private float horizontalAxis;
    private Rigidbody2D rb2d;
    
    private BoxCollider2D boxcollider;
    private CapsuleCollider2D capCollider;
    private bool canJump;
    private bool grounded;
    private bool walking;
    private float addedJump = 0f;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        capCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        grounded = IsGrounded();
        if(anim != null)
            anim.SetBool("Grounded", grounded);
    }

    void Update()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");

        if(horizontalAxis == -1f && visuals.transform.localScale.x < 0)
        {
            visuals.transform.localScale = new Vector3(visuals.transform.localScale.x * -1, visuals.transform.localScale.y, visuals.transform.localScale.z);
        }else if(horizontalAxis == 1f && visuals.transform.localScale.x > 0)
        {
            visuals.transform.localScale = new Vector3(visuals.transform.localScale.x * -1, visuals.transform.localScale.y, visuals.transform.localScale.z);
        }

        grounded = IsGrounded();
        if (anim != null)
            anim.SetBool("Grounded", grounded);


        if (Input.GetButtonDown("Jump") && grounded && this.gameObject.layer == 11)
        {
            JumpSFX.Play();
            Jump(jumpForce);

        }

        if (Input.GetAxisRaw("Vertical") == -1)
        {
            this.gameObject.layer = 12;
        }
        else
        {
           this.gameObject.layer = 11;
        }

        if (!walking && (horizontalAxis == 1f || horizontalAxis == -1f))
        {
            walking = true;
            if (anim != null)
                anim.SetBool("Walking", true);
        }
        else if (walking && horizontalAxis == 0f)
        {
            walking = false;
            if (anim != null)
                anim.SetBool("Walking", false);
        }

        
    }

    private void FixedUpdate()
    {

        float xMove = horizontalAxis * moveSpeed * Time.deltaTime;
        rb2d.velocity = new Vector2(xMove, rb2d.velocity.y);
     
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Pad")
        {
            canJump = false;
        }

        if (collision.gameObject.tag == "Platform" && Input.GetAxisRaw("Vertical") == -1)
        {
            Debug.Log("Down");
            //collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            
        }
        else
        {
           // this.gameObject.layer = 11;
        }
    }




    public void Jump(float force)
    {
        grounded = false;
        canJump = false;

        if (anim != null)
        {
            anim.SetBool("Grounded", false);
            anim.SetTrigger("Jump");
        }
            
        //rb2d.AddForce(new Vector2(0, force), ForceMode2D.Impulse);

        rb2d.velocity = new Vector2(rb2d.velocity.x, force);
        //addedJump = 0f;
    }

    public void Launch(float force)
    {
        Debug.Log("LAUNCH");

        grounded = false;
        canJump = false;

        if (anim != null)
        {
            anim.SetBool("Grounded", false);
            anim.SetTrigger("Jump");
        }

        //rb2d.AddForce(new Vector2(0, force), ForceMode2D.Force);

        if (rb2d.velocity.y < force)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, force);
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capCollider.bounds.center, capCollider.bounds.size + new Vector3(0.05f, 0f, 0f), 0f, Vector2.down, jumpCheckOffset, groundLayers);
        //RaycastHit2D raycastHit = Physics2D.BoxCast(capCollider.bounds.center, capCollider.bounds.size + new Vector3(0.05f, 0f, 0f), 0f, Vector2.down, jumpCheckOffset, groundLayers);

        Color rayColor;
        if(raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(capCollider.bounds.center + new Vector3(capCollider.bounds.extents.x, 0), Vector2.down * (capCollider.bounds.extents.y + jumpCheckOffset), rayColor);
        Debug.DrawRay(capCollider.bounds.center - new Vector3(capCollider.bounds.extents.x, 0), Vector2.down * (capCollider.bounds.extents.y + jumpCheckOffset), rayColor);
        Debug.DrawRay(capCollider.bounds.center - new Vector3(capCollider.bounds.extents.x, capCollider.bounds.extents.y + jumpCheckOffset), Vector2.right * (capCollider.bounds.extents.x *2), rayColor);

        return raycastHit.collider != null && raycastHit.collider.isTrigger == false;
    }
}
