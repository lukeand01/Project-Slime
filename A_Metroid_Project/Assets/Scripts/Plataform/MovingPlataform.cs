using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlataform : MonoBehaviour
{
    ///THIS MOVES THE PLATAFORM. IT CAN EITHER GO DOWN-UP. OR SIDEWAYS. 
    //
    [SerializeField] bool updown;
    [SerializeField] float distance;
    [SerializeField] float speed;
    [SerializeField] float pauseTime; //THIS IS THE PAUSE WHEN IT REACHS IT LIMIT. HOW LONG IT TAKES TO RETURN IN THE PATH.

    Vector2 originalPos;

    private void Start()
    {
        originalPos = transform.position;
        if (updown)
        {
            StartCoroutine(DownUp());
        }
        else
        {
            StartCoroutine(GoSide());
        }
    }

    IEnumerator DownUp()
    {
        //WE GO DOWN.
        //WE WAIT THERE.
        //WE GO UP.
        //REPEAT PROCESS.

        transform.DOMoveY(originalPos.y + distance, speed);

                      
        while (transform.position.y < originalPos.y + distance)
        {

            yield return null;
        }

        yield return new WaitForSeconds(pauseTime);
        transform.DOMoveY(originalPos.y, speed);

        while (transform.position.y - originalPos.y > 1)
        {

            yield return null;
        }


        yield return new WaitForSeconds(pauseTime);

        StartCoroutine(DownUp());
    }

    IEnumerator GoSide()
    {
        yield return null;
    }

}
