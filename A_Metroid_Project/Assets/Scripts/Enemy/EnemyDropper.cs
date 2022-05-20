using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropper : MonoBehaviour, IInteractable
{
    //IF THE ENENMY HAS THIS. IT WONT BE DESTROYED WHEN IT IS KILLED.
    //THIS WORKS AS AN INTERACT.



    [SerializeField] GameObject interactUI;




    public string GetGuid()
    {
        return null;
    }

    public bool Interact()
    {
        //THE INTERACT OF THIS WILL BE GAINING SOMETHING.


        return true;
    }

    public void InteractUI(bool choice)
    {
        interactUI.SetActive(choice);   
    }
}
