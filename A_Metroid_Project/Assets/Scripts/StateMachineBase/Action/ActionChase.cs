using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI / Action / Chase")]
public class ActionChase : Action
{
    public override void Act(StateController controller)
    {
        //JUST GO TOWARDS TEH PLAYER.
        //SHOULDT
        Chase(controller);


        float directionChecker = (controller.playerReference.transform.position.x - controller.transform.position.x);

        if(controller.direction == 1 && directionChecker < 0)
        {
            controller.direction = -1;
            controller.RotateSprite();
        }
        if(controller.direction == -1 && directionChecker > 0)
        {
            controller.direction = 1;
            controller.RotateSprite();
        }

    }
    public override void Begin(StateController controller)
    {
        
    }
    void Chase(StateController controller)
    {
        Vector2 player = new Vector2(controller.playerReference.transform.position.x, controller.transform.position.y);
        controller.transform.position = Vector2.MoveTowards(controller.transform.position, player, Time.deltaTime * controller.enemyBase.runSpeed);

    }

}
