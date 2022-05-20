using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI / Decision / InRange")]
public class DecisionInRange : Decision
{
    public override bool Decide(StateController controller)
    {
        bool theBool = Decision(controller);

        return theBool;

    }


    bool Decision(StateController controller)
    {
        if (Vector2.Distance(controller.transform.position, controller.playerReference.transform.position) <= controller.enemyBase.attackRange)
        {
            return true;
        }

        return false;

    }
}
