using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventDie : MonoBehaviour
{
    ///WHEN THE SELECTED CHARACTERS DIE. WE DO SOMETHING

    public StateController[] enemiesToDie;
    public UnityEvent trigger;

    private void Update()
    {
        if (AllDead())
        {
            CallEnd();
        }
    }


    bool AllDead()
    {
        for (int i = 0; i < enemiesToDie.Length; i++)
        {
            if(enemiesToDie[i].die == true)
            {
                return true;
            }


        }

        return false;
    }

    void CallEnd()
    {
        trigger.Invoke();
        enabled = false;
    }

}
