using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTimeline : MonoBehaviour
{
    StateController controller;
    private void Start()
    {
        controller = GetComponent<StateController>();
        Observer.instance.EventTimeline += HandleTimeline;
    }

    private void OnDestroy()
    {
        Observer.instance.EventTimeline += HandleTimeline;
    }

    void HandleTimeline(bool choice)
    {
        Debug.Log("enemy handle timeline");
        //THIS MEANS THAT THE THING IS NOT RUNNING.
        controller.enabled = choice;
    }
}
