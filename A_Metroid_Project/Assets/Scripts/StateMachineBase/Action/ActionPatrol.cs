using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI / Action / BasicPatrol")]
public class ActionPatrol : Action
{
    [SerializeField] bool canJump;

    int direction = 1;

    public override void Act(StateController controller)
    {       
        HandleMove(controller);
    }

    void HandleMove(StateController controller)
    {
        controller.Move(controller.enemyBase.walkSpeed);

        if (ShouldJump())
        {

        }


        if (controller.ShouldTurn())
        {
            //THEN THE ONLY THING WE DO IS CHANCE THE NUMBER.

            controller.direction *= -1;

        }



    }

   

    bool ShouldJump()
    {
        //WONT BOTHER WITH THIS FOR NOW.
        return false;
    }


    public override void Begin(StateController controller)
    {
        
    }
}

