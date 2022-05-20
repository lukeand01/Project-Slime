using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatSlime : EnemyCombat
{
    bool canAttack = true;

    bool action = true;

    public override void Act()
    {
        //NOW WE DO EVERYTHING HERE.

        if (canAttack)
        {
            canAttack = false;
            StartCoroutine(Attack());
           
        }
    }

    IEnumerator Attack()
    {
        //I WANT HIM TO NOT MOVE.

        controller.attacking = true;
        anim.Play("Slime_Attack");
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("Slime_Attack"))
        {
            yield return null;
        }
        action = false;
        yield return new WaitForSeconds(controller.enemyBase.attackSpeed / 2);
        controller.attacking = false;
        yield return new WaitForSeconds(controller.enemyBase.attackSpeed / 2);
        
        canAttack = true;
    }


}
