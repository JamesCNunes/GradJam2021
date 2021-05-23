using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent OnHit;
    public UnityEvent OnDie;

    [SerializeField] private int maxHealth = 1;
    private int curHealth;

    private void Start()
    {
        curHealth = maxHealth;
    }

    public void TakeDamage()
    {
        

        if(curHealth > 0)
        {
            curHealth--;
            Debug.Log("Player Damaged");
            OnHit?.Invoke();
        }

        if(curHealth <= 0)
        {
            Die();

        }


    }

    private void Die()
    {
        OnDie?.Invoke();
    }


}
