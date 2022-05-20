using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject interactUI;

    public string GetGuid()
    {
        return null;
    }

    public bool Interact()
    {
        return true;
    }

    public void InteractUI(bool choice)
    {
        interactUI.SetActive(choice);
    }
}
