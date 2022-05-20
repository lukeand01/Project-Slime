using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI / Action / Combat")]
public class ActionCombat : Action
{
    public override void Act(StateController controller)
    {
        controller.enemyCombat.Act();

    }
    public override void Begin(StateController controller)
    {
        
    }
}
