using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    public string GetGuid();
    public bool Interact();
    public void InteractUI(bool choice);
}
