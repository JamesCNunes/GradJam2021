using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveHeart : MonoBehaviour
{
    [SerializeField] private List<Image> hearts;

    int hIndex;

    private void Start()
    {
        hIndex = hearts.Count;
    }

    public void DisableHeart()
    {
        if(hIndex > 0)
        {
            hIndex--;
            hearts[hIndex].enabled = false;
        }
    }
}
