using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI / Action / Jumper / Combat")]
public class ActionCombatJumper : Action
{
    public override void Act(StateController controller)
    {
        
    }

    public override void Begin(StateController controller)
    {       
        controller.GetComponent<JumperMovement>().ChangeState(JumperMovement.JumperStates.Attack);
    }
}
