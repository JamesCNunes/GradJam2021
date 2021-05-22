using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]private Health enemyHealth;
    [SerializeField] private float playerForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<MoveController>().Launch(playerForce);
            enemyHealth.TakeDamage();
            Debug.Log("Hit ENEMY");
        }
    }
}
