using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatSentry : EnemyCombat
{
    [SerializeField] bool targetting;
    [SerializeField] bool ShootUp; //IF IT IS FALSE. WE SHOOT TOWARDS THE ENEMYM PLAYER.
    [SerializeField] float shootCooldown;
    bool completed;

    [SerializeField] GameObject projectil;
    [SerializeField] GameObject eye;
    
    //MAYBE WE HAVE A FLOAT THAT GOES TO 90.
    //THATS THE ANGLE OF THE SHOT.


    private void Awake()
    {
        completed = true;
    }
    public override void Act()
    {
        //WE SEE PLAYER. WE JUST START RANDOMLY SHOOTING UP OR NOT.
        //AS ALWAYS, WE ONLY ACT IF WE CAN.
        if (!completed) return;
        completed = false;
        StartCoroutine(SentryProcess());
    }

    private void Start()
    {
        StartCoroutine(SentryProcess());
    }
    public int teste;
    IEnumerator SentryProcess()
    {
        //IT SHOULD SHOOT UP.
        //THE RANDOM VALUE REPRESENTS AN ARC ABOVE THE SENTRY.

        //int randomChoice = Random.Range(0, 5);

        Shoot(GetDirection());

        yield return new WaitForSeconds(shootCooldown);
        completed = true;
        StartCoroutine(SentryProcess());
    }

    void Shoot(Vector3 direction)
    {
        GameObject newObject = Instantiate(projectil, eye.transform.position, Quaternion.identity);
        Enemy GetEnemy = GetComponent<StateController>().enemyBase;
        newObject.GetComponent<ProjectilSentry>().SetUp(GetEnemy.damage, 2);
        newObject.GetComponent<Rigidbody2D>().AddForce(direction);
    }

    Vector3 GetDirection()
    {
        //IT SHOULD GO EITHER SIDE, AND A BIT UP.
        //IT NEVER COMES FROM BELOW THE SENTRY. IT NEVER GOES COMPLETELY UP.
        int randomValueX = Random.Range(-500, 500);
        int randomValueY = Random.Range(-500, 500);


        return new Vector3(randomValueX, randomValueY, 0);
    }

}
