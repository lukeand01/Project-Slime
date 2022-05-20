using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1 : EventBase
{
    //SHOULD I DO IT THROUGH DIFFERENT MONOBEHAVIOR EVENTS.?
    //LETS DO THIS ONE AS A TEST.

    public List<DialogueClass> dialogueClasslist = new List<DialogueClass>();
    

    public override void Commence(PlayerHandler player)
    {
        //WE START
        started = true;

        DialogueHandler.instance.StartRegularNarration("Now lets see if we can reach that light");              

    }

    public override void Return(PlayerHandler player)
    {
        //WE DONT LET HIM PASS UNLESS HE HAS COMPLETED THE TASK.
        if (completed)
        {
            //THEN WE DISABLE THIS EVENT. 
            Destroy(gameObject);
        }
        if (!completed)
        {
            DialogueHandler.instance.StartRegularNarration("lets try jumping that before going anywhere");
            //THEN WE ARE PUSHED BACK.
            MoveCharacter(player, -1, 2f);
        }
    }

    int jumpToTry = 3;
    int jumpTried = 0;
    private void Update()
    {
        if (!started) return;
        if (Input.GetKeyDown(PlayerHandler.instance.input.jump)) 
        {
            jumpTried += 1;

            if(jumpTried == 1)
            {
                DialogueHandler.instance.StartRegularNarration("Jumper higher!");
                return;
            }
            if(jumpTried == 2)
            {
                DialogueHandler.instance.StartRegularNarration("That cant possible all you can! jump again!");
                return;
            }
            if(jumpTried == 3)
            {             
                DialogueHandler.instance.StartRegularNarration("Ok, looks like you cant jump far. we need to try another strategy");
                Observer.instance.OnEventProced(targetId);
                completed = true;
                enabled = false;
               
            }

        }
    }

}

