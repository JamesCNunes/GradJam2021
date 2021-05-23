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

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (moveLeft)
        {

            if(gameObject.transform.localScale.x < 0)
            {
                gameObject.transform.localScale = new Vector3(-1 * gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
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

            if (gameObject.transform.localScale.x > 0)
            {
                gameObject.transform.localScale = new Vector3(-1 * gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
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
