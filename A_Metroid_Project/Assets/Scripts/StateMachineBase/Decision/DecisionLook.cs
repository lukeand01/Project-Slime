using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI / Decision / Look")]
public class DecisionLook : Decision
{
    public override bool Decide(StateController controller)
    {
        bool newBool = Look(controller);
        return newBool;
    }



    //THERE SHOULD BE X SPOT AND Y SPOT.
    bool Look(StateController controller)
    {

        if (controller.combat) return true;

      
        if(Vector2.Distance(controller.playerReference.transform.position, controller.transform.position) <= controller.enemyBase.spotRange)
        {           
            if(controller.playerReference.transform.position.y - controller.transform.position.y > 2)
            {
                return false;
            }


            return true;
        }
        return false;
    }
}
