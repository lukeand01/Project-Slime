using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonSystem : MainMenuButton
{
    [SerializeField] UnityEvent trigger;

    public override void OnPointerClick(PointerEventData eventData)
    {
        trigger.Invoke();
    }

    
}
