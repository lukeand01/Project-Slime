using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour, IInteractable
{
    //YOU CAN TALK WITH NPC.
    //NPC MAY GRANT YOU AN ABILITY, KEY OR TRIGGER AN EVENT.

    public List<TextAsset> dialogueList = new List<TextAsset>();
    int currentDialogue;

    GameObject interactUI;

    private void Start()
    {
        interactUI = transform.GetChild(0).gameObject;
    }


    public string GetGuid()
    {
        //WE DONT NEED THIS HERE.
        return null;
    }

    public bool Interact()
    {
        //WE START THE CURRENT DIALOGUE.

        //START CONVERSATION.
        //
        //EVERYTIME A 
        DialogueHandler.instance.StartDialogue(dialogueList[currentDialogue]);

        return true;
    }

    public void InteractUI(bool choice)
    {
        //WE SIMPLY SHOW THE UI ABOVE THE HEAD.
        interactUI.SetActive(choice);
    }
}
