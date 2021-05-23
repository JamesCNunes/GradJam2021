using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBehavior : MonoBehaviour
{

    [SerializeField]private float speed;
    [SerializeField]private float groundCheckLength;
    [SerializeField]private bool moveLeft;
    [SerializeField]private BoxCollider2D boxCol;
    [SerializeField]private LayerMask mask;
    [SerializeField]private GameObject visuals;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (moveLeft)
        {

            if(visuals.transform.localScale.x < 0)
            {
                visuals.transform.localScale = new Vector3(-visuals.transform.localScale.x, visuals.transform.localScale.y, visuals.transform.localScale.z);
            }
            rb.velocity = new Vector2(-speed, rb.velocity.y);

            RaycastHit2D hit = Physics2D.Raycast(new Vector2(boxCol.bounds.min.x, boxCol.bounds.min.y), Vector2.down, groundCheckLength, mask);
            Debug.DrawRay(new Vector2(boxCol.bounds.min.x, boxCol.bounds.min.y), Vector2.down * groundCheckLength, Color.red);
            
            if (hit.collider == null)
            {
                moveLeft = false;
            }
        }
        else
        {

            if (visuals.transform.localScale.x > 0)
            {
                visuals.transform.localScale = new Vector3(-visuals.transform.localScale.x, visuals.transform.localScale.y, visuals.transform.localScale.z);
            }
            rb.velocity = new Vector2(speed, rb.velocity.y);

            RaycastHit2D hit = Physics2D.Raycast(new Vector2(boxCol.bounds.max.x, boxCol.bounds.min.y), Vector2.down, groundCheckLength, mask);
            Debug.DrawRay(new Vector2(boxCol.bounds.max.x, boxCol.bounds.min.y), Vector2.down * groundCheckLength, Color.red);

            if (hit.collider == null)
            {
                moveLeft = true;
            }
        }
    }
}
