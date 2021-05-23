using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhaser : MonoBehaviour
{
    bool timerOn;
    [SerializeField] float delay = 1f;
    float timer;



    public void Phase()
    {
        SetLayerRecursively(this.gameObject, 14);
        timer = delay;
        timerOn = true;
    }

    private void Update()
    {
        if (timerOn)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                timerOn = false;
                SetLayerRecursively(this.gameObject, 10);
            }
        }
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
