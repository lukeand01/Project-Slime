using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI / Action / Jumper / Chase")]
public class ActionChaseJumper : Action
{
    public override void Act(StateController controller)
    {
        
    }

    public override void Begin(StateController controller)
    {
        controller.GetComponent<JumperMovement>().ChangeState(JumperMovement.JumperStates.Chase);
    }
}
