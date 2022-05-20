using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    //WILL PUT THIS AND FILL WITH WHATEVER I NEED FOR AECH CHARACTER.
    [HideInInspector] public Animator anim;
    [HideInInspector] public StateController controller;
    private void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        controller = GetComponent<StateController>();
    }

    public virtual void Act()
    {
        //THIS IS WHERE WE DO EVERYTHING.
    }

    public virtual void Die()
    {
        //THIS DISABLES THE ENEMY COMBAT ENEMY.
        this.enabled = false;


    }
}
