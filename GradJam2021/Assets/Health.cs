using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent OnHit;

    [SerializeField] private int maxHealth = 1;
    private int curHealth;

    private void Start()
    {
        curHealth = maxHealth;
    }

    public void TakeDamage()
    {
        curHealth--;

        if(curHealth > 0)
        {
            OnHit.Invoke();

        }
        else
        {
            Die();
        }

    }

    private void Die()
    {
        Destroy(this.gameObject);
    }


}
