using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event2 : EventBase
{
    public override void HandleEventInfo(int _id)
    {


        base.HandleEventInfo(_id);
        //

        //WE SHOULD NEVER DESTROY
       enabled = false;

    }

    public override void Commence(PlayerHandler player)
    {
        //THIS CHANGES NOTHING. IT ALWAYS SAY THE SAME.
        DialogueHandler.instance.StartRegularNarration("I think we should check that rock first");
        MoveCharacter(player, -1, 1.5f);

    }
}
