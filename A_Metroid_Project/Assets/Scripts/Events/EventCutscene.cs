using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
public class EventCutscene : EventBase
{
    //EVENT CUTSCENE
    public TimelineAsset cutSceneClip;

    //IF YOU ARE JUMPING THEN YOU SHOULD STOP. COME DOWN IN THE PLACE.


    public override void Commence(PlayerHandler player)
    {
        //BLOCK INPUT.
        //START THE TIMELINE.
        MakePlayerIdle(player);

        player.ChangePermission(InputPermission.Blocked);
        GameHandler.instance.PlayNewTimeline(cutSceneClip);
        enabled = false;
    }

    void MakePlayerIdle(PlayerHandler player)
    {
        player.rb.velocity = new Vector2(0, 0);


    }
   

}
