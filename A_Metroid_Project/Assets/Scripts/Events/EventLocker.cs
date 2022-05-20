using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLocker : EventBase
{
    //THIS IS THE TYPE THAT BLOCKS ANOTHER EVENT TILL A CONDITION WAS MET.
    //WE GET ALL EVENTS IN THIS OBJECT AND ENABLE THEM.

    EventBase[] events;

    private void Start()
    {
        events = GetComponents<EventBase>();   
    }

    public void UnlockEvent()
    {
        for (int i = 0; i < events.Length; i++)
        {
            events[i].enabled = true;
        }
    }


    public override void Commence(PlayerHandler player)
    {
        //WE START
    }

    public override void Return(PlayerHandler player)
    {
        //WE DONT LET HIM PASS UNLESS HE HAS COMPLETED THE TASK.
    }


    
}
