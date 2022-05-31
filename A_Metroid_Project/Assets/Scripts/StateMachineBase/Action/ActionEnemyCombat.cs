using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEnemyCombat : Action
{
    public override void Act(StateController controller)
    {
        //NOTHING HERE.

    }

    public override void Begin(StateController controller)
    {
        controller.GetComponent<EnemyCombat>().Act();
    }
}
