using System.Collections;
using System.Collections.Generic;
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

    private Mammoth state = Mammoth.Charging;
    bool facingLeft;

    [SerializeField] private BoxCollider2D box;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float chargeSpeed;
    [SerializeField] private float checkDistance;
    [SerializeField] private LayerMask mask;

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
            

            facingLeft = !facingLeft;
            this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x * -1, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
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
}
