using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Observer : MonoBehaviour
{
    public static Observer instance;

    private void Awake()
    {
        instance = this;
    }




    #region EVENTS 
    public event Action<int> EventEventProced;
    public void OnEventProced(int id) => EventEventProced?.Invoke(id);
    #endregion

    #region CAMERA
    public event Action<bool, Transform> EventCameraJump; 
    public void OnCameraJump(bool choice, Transform pos) => EventCameraJump?.Invoke(choice, pos);
    #endregion

    #region UI
    public event Action<int> EventHandlePause;
    public void OnHandlePause(int empty) => EventHandlePause?.Invoke(empty);

    public event Action<int> EventDeathMenu;
    public void OnDeathMenu(int empty) => EventDeathMenu?.Invoke(empty);

    #endregion

    #region TIMELINE

    public event Action<bool> EventTimeline;
    public void OnTimeline(bool startedOrNot) => EventTimeline?.Invoke(startedOrNot);

    #endregion

}
