using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float force;
    Rigidbody2D rb;

    [SerializeField] private Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("Bounce");
            collision.gameObject.GetComponent<MoveController>().Launch(force);
        }
    }

}
