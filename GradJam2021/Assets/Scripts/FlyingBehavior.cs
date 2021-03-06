using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBehavior : MonoBehaviour
{
    [SerializeField]private float timePeriod;
    [SerializeField]private float height;
    [SerializeField]private float timeSinceStart;
    [SerializeField]private AudioSource source;
    [SerializeField]private GameObject visuals;
    [SerializeField]private BoxCollider2D col;

    bool isDead;

    private Vector2 pivot;
    private void Start()
    {
        pivot = transform.position;
        height /= 2;
        timeSinceStart = (3 * timePeriod) / 4;
    }

    void Update()
    {
        //Vector3 mov = new Vector3(transform.position.x, Mathf.Sin(speed * Time.time) * distance + transform.position.y, transform.position.z);
        //transform.position = mov;

        if (isDead)
        {
            return;
        }

        Vector2 nextPos = transform.position;
        nextPos.y = pivot.y + height + height * Mathf.Sin(((Mathf.PI * 2) / timePeriod) * timeSinceStart);
        timeSinceStart += Time.deltaTime;
        transform.position = nextPos;
    }

    public void Die()
    {
        source.Play();
        visuals.SetActive(false);
        isDead = true;
        col.enabled = false;
        this.enabled = false;
        
    }
}
