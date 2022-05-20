using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCommentary : EventBase
{
    //

    [TextArea]
    public List<string> commentaries;


    public override void Commence(PlayerHandler player)
    {
        //JUST ONE THING

        Destroy(this);
    }

}
