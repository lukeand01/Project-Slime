using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PileRocks : MonoBehaviour, IInteractable
{
    //
    public int rocks;
    [SerializeField] GameObject interactUI;
    string id = "";

    private void Start()
    {
        id = Guid.NewGuid().ToString();
    }

    public string GetGuid()
    {

        return id;
    }

    public bool Interact()
    {


        PlayerHandler.instance.combat.GainRock(rocks);
        Destroy(gameObject);
        return true;
    }

    public void InteractUI(bool choice)
    {
        //MAYBE PLAY ANIMATION HERE.
        interactUI.SetActive(choice);
    }

  
}
