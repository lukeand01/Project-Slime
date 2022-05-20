using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSave : EventBase
{

    public override void Commence(PlayerHandler player)
    {

        SaveHandler.instance.Save();
        enabled = false; //AFTER SAVING WE DISABLE THE OBJECT SO THAT WE CANT KEEP SAVING.
    }

}
