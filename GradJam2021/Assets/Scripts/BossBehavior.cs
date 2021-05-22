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

    private Mammoth state = Mammoth.WaitToStart;


    private void Update()
    {
        switch (state)
        {
            case Mammoth.WaitToStart:
                break;
            case Mammoth.Idle:
                break;
            case Mammoth.Charging:
                break;
            case Mammoth.Stunned:
                break;
            case Mammoth.Recover:
                break;
            case Mammoth.Dead:
                break;

        }
    }

    public Mammoth GetState()
    {
        return state;
    }
}
