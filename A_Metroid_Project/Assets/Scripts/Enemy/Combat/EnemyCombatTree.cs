using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyCombatTree : EnemyCombat
{
    //THIS IS INDEPENDENT.
    ///IT GOES DOWN, THEN RETURNS, THEN GOES TO THE SIDE AND THE PROCESS REPEATS.
    ///IT DEALS DAMAGE WHEN ITS GOING DOWN, BUT NOT WHEN ITS MOVING SIDE OR MOVING UP.

    BoxCollider2D bottomCollider;
    float originalPosX;



    private void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        originalPosX = transform.position.x;
    }

    private void Start()
    {
        //I CHANGE THE LAYER OF THE BOTTOM COLLIDER TO ENEMY.
        StartCoroutine(GoDown());
    }

    int teste;
    IEnumerator GoDown()
    {
        transform.GetChild(0).gameObject.layer = 8;
        anim.Play("TreeDown");

        while(anim.GetCurrentAnimatorStateInfo(0).IsName("TreeDown") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f){
            teste += 1;
            if(teste == 10)
            {
                Debug.Log("something went wrong. about to crash");
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(3);
        StartCoroutine(GoUp());
    }

    IEnumerator GoUp()
    {
        anim.Play("TreeUp");
        transform.GetChild(0).gameObject.layer = 0;

        while (anim.GetCurrentAnimatorStateInfo(0).IsName("TreeDown") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            teste += 1;
            if (teste == 10)
            {
                Debug.Log("something went wrong. about to crash");
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(2);

        StartCoroutine(GoSide());
    }


    bool goRight;
    bool once;
    IEnumerator GoSide()
    {
        //GO TO RIGHT OR LEFT.
        //I DONT NEED TO CHECK IF IT CAN, BECAUSE I JUST NEED TO PUT IN THE RIGHT POSITIONS.
        once = false;
        if (goRight && !once)
        {
            transform.DOMoveX(transform.position.x + 1, 1);         
            goRight = false;
            once = true;
        }
        if (!goRight && !once)
        {
            transform.DOMoveX(transform.position.x - 1, 1);
            goRight = true;
            once = true;
        }

        yield return new WaitForSeconds(2);
        StartCoroutine(GoDown());
    }

}
