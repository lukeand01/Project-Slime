using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : EventBase
{
    ///THIS JUST SERVES TO CALL A ESPECIFIC FUNCTION IN CASE IT IS TOUCHED.
    public UnityEvent trigger;

    public override void Commence(PlayerHandler player)
    {
        trigger.Invoke();
        enabled = false;
    }

}
