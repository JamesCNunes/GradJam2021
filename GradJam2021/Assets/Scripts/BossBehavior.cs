using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public enum Mammoth { 
        WaitToStart,
        Idle,
        Charging,
        Stunned,
        Recover,
        Dead
    }

    private Mammoth state = Mammoth.WaitToStart;
    bool facingLeft;

    [SerializeField]private float waitBeforeCharge = 1f;
    private float chargeTimer;

    private float endTimer = 4;

    [SerializeField] private BoxCollider2D box;
    [SerializeField] private BoxCollider2D trigBox;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private float chargeSpeed;
    [SerializeField] private float checkDistance;
    [SerializeField] private LayerMask mask;
 //   [SerializeField] private AudioSource roar;

    private void Update()
    {
        switch (state)
        {
            case Mammoth.WaitToStart:
                break;
            case Mammoth.Idle:
                break;
            case Mammoth.Charging:
                Charging();
                break;
            case Mammoth.Stunned:
                Stunned();
                break;
            case Mammoth.Recover:
                break;
            case Mammoth.Dead:
                break;

        }
    }

    public void StartBattle()
    {
        state = Mammoth.Charging;
    }

    public Mammoth GetState()
    {
        return state;
    }

    private void Charging()
    {

        RaycastHit2D hit;

        if (facingLeft)
        {
            hit = Physics2D.Raycast(new Vector2(box.bounds.max.x, box.bounds.center.y), Vector2.left, checkDistance, mask);
            Debug.DrawRay(new Vector2(box.bounds.max.x, box.bounds.center.y), Vector2.left * checkDistance, Color.red);
        }
        else
        {
            hit = Physics2D.Raycast(new Vector2(box.bounds.min.x, box.bounds.center.y), Vector2.right, checkDistance, mask);
            Debug.DrawRay(new Vector2(box.bounds.min.x, box.bounds.center.y), Vector2.right * checkDistance, Color.red);
        }

        if(hit.collider != null)
        {
            //Hit the wall
            //Stop

            facingLeft = !facingLeft;
            rb.velocity = Vector3.zero;
            this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x * -1, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
            StunMammoth();
            return;
        }
        

        if (facingLeft)
        {
            rb.velocity = new Vector2(chargeSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-chargeSpeed, rb.velocity.y);
        }

    }

    void StunMammoth()
    {
        
        anim.SetTrigger("Idle");
        chargeTimer = waitBeforeCharge;
        state = Mammoth.Stunned;
    }

    void Stunned()
    {
        chargeTimer -= Time.deltaTime;

        if(chargeTimer <= 0)
        {
            anim.SetTrigger("Charge");
            state = Mammoth.Charging;
        }
    }

    public void Death()
    {
        trigBox.enabled = false;
        box.enabled = false;
        rb.velocity = Vector3.zero;
        state = Mammoth.Dead;
        anim.SetTrigger("Idle");
        
        //sound
    }

    void Dead()
    {
        endTimer -= Time.deltaTime;

        if(endTimer <= 0)
        {
            //End Game

        }
    }
}
