using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Enemy : ScriptableObject
{
    //THIS HOLDS ALL INFORMATION ABOUT THE FELLA.
    [Header("BASE")]
    public string enemyName;
    public float maxHealth;

    [Header("MOVEMENT")]
    public float walkSpeed; //THIS IS A MODIFIER OF ONE.
    public float runSpeed;

    [Header("RANGE")]
    public float patrolRange;
    public float spotRange;
    public float attackRange;

    [Header("JUMP")]
    public bool jumpPatrol;
    public bool jumpChase;

    [Header("COMBAT")]
    public float attackSpeed;
}

//HOW TO HANDLE ATTACKS?