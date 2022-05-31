using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatDasher : EnemyCombat
{
    //THE DASHER STOPS FOR A SECOND. AND CHARGES QUICKLY.
    //WHILE HE CHARGES: HE SHOULD STOP BEFORE FALLING FROM LEDGE OR IF IT HITS WALL.
    //IT BECOMES UNTARGETABLE. USE A NEW LAYER CALLED INVISIBLE.

    [SerializeField] bool onCooldown; //ITS VERY SHORT.

    public override void Act()
    {
        //IF I AM IN RANGE
        StartCoroutine(DashAttack());
        Debug.Log("time to act");
    }

    private void Update()
    {
        //I CAN CHECK HERE. IF HE IS EVER STUNNED WE STOP ALL COUROTINES
        //STILL GOES THROUGH 


        if (controller.LedgeOrWall()) //OR IFS STUNNED
        {
            StopAllCoroutines();
            //IF WE STOP WE NEED TO TELL THE THING TO ADJUST AND TRY AGAIN.
        }

    }

    //EVERYTIME WE ARE TO ACT WE THINK IF WE SHOULD KEEP DOING IT. STUN AND PLAYER BEING OUT OF REACH.

    IEnumerator DashAttack()
    {

        //CHARGE AND THEN ATTACK.
        //AFTER WE ARE DONE. WE CHECK IF THERE PLAYER IS IN RANGE. OTHERWISE WE DONT CHARGE AGAIN.
        controller.attacking = true;

        Debug.Log("i ready to charge");
        yield return new WaitForSeconds(2);
        Charge();
        Debug.Log("i charge");
        yield return new WaitForSeconds(1);

        controller.attacking = false;
        //
        yield return null;
    }

    void Charge()
    {
       
        
    }

}
