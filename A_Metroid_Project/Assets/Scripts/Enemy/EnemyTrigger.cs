using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyTrigger : MonoBehaviour
{
    //THIS DOES SOMETHING WHEN IT DIES.
    [SerializeField] UnityEvent trigger;

    //FOR NOW I WONT BOTHER WITH GIVING DATA THROUGH THE UNITY EVENT.


    public void SetTrigger(UnityEvent newTrigger)
    {
        trigger = newTrigger;
    }

    public void UseTrigger()
    {
        if (trigger == null) return;

        Debug.Log("the trigger was used");

        trigger.Invoke();
    }


}
