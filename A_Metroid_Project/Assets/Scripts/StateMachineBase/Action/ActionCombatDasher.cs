using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI / Action / Dasher / Combat")]
public class ActionCombatDasher : Action
{
    ///I AM USING DASHER FOR BASIC COMBAT STUFF.
   

    public override void Act(StateController controller)
    {
        controller.GetComponent<EnemyCombat>().Act();
    }

    public override void Begin(StateController controller)
    {
        //THIS IS ONE TIME.

        controller.GetComponent<EnemyCombat>().Act();

    }
}
