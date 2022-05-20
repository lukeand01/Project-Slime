using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI / Action / Jumper / Patrol")]
public class ActionPatrolJumper : Action
{
    //THE DIFFERENCE IS THAT HE JUMPS TO GET TO PLACES.
    //

    public override void Act(StateController controller)
    {
        //IN HERE WE TELL THE 
        //WE DO NOTHING HERE
        
    }

    public override void Begin(StateController controller)
    {
        controller.GetComponent<JumperMovement>().ChangeState(JumperMovement.JumperStates.Patrol);
    }
}
