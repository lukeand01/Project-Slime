using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperMovement : EnemyCombat
{
    //ITS HERE WE ACTUALL GOING TO MOVE THE JUMPER ENEMY.


    float currentJumpCooldown;

    float currentJumpHeight; 


    [Header("JUMPER PATROL")]
    public float patrolJumpHeight = 7;
    public float patrolJumpCooldown;

    [Header("JUMPER CHASE")]
    public float chaseJumpHeight;
    public float chaseJumpCooldown;

    [Header("JUMPER COMBAT")]
    public float combatJumpHeight;
    public float combatJumpCooldown;


     
    float direction = 1;

    JumperStates currentStates;
    public enum JumperStates
    {
        Patrol,
        Chase,
        Attack
    }



    private void Awake()
    {
        controller = GetComponent<StateController>();
    }

    private void Start()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        //WE HAVE JUMP COOLDOWN.
        //Debug.Log("This handle was called in the state " + currentStates);
        if(currentStates == JumperStates.Patrol)
        {
            //MIDDLE JUMPS AND LONG COOLDOWNS.
            HandlePatrol();
            return;
        }
        if (currentStates == JumperStates.Chase)
        {
            //THIS WE MOVE TO THE TARGET BUT FASTER. LESS COOLDOWN AND SHORT JUMPS.
            HandleChase();
            return;
        }
        if (currentStates == JumperStates.Attack)
        {
            //LONG JUMP TARGETTED AT THE PLAYER.
            Debug.Log("got sent here");
            HandleAttack();
            return;

        }

    }

    void HandlePatrol()
    {
        //WE SET UP THE STATS. 
        //THEN WE CALL.
        currentJumpCooldown = patrolJumpCooldown;
        currentJumpHeight = patrolJumpHeight;

        //I HAVE TO TELL THE DIRECTION.
        //THE DIRECTION IS DEFINED BY PATROL ROUTE.

        //IT IS DEFINED BY PLAYER DIRECTION.

        //WE CHECK IF THERE ARE ANY REASONS TO TURN AROUND. WALL, LEDGE OR TOO FAR.

        if (controller.ShouldTurn())
        {
            direction *= -1;
        }

        StartCoroutine(ProcessJump());
        //IN THE END WE ALWAYS 
    }
    void HandleChase()
    {
        currentJumpCooldown = chaseJumpCooldown;
        currentJumpHeight = chaseJumpHeight;
        Debug.Log("chase got called");
        
        //FIRST I CARE ONLY ABOUT THE DIRECTION NOT THE DISTANCE.
        Vector3 directionTowardsPlayer =  (controller.playerReference.transform.position - controller.transform.position).normalized;



        if(directionTowardsPlayer.x > 0)
        {
            direction = 1;
        }
        if(directionTowardsPlayer.x < 0)
        {
            direction = -1;
        }

        StartCoroutine(ProcessJump());

    }
    void HandleAttack()
    {
        Debug.Log("handle attack");

        currentJumpCooldown = combatJumpCooldown;
        currentJumpHeight = combatJumpHeight;

        //THEIR ATTACK IS THAT THEY AND THEN JUMP REALLY HIGH IN THE PLAYER POSITION

        float newDirection = controller.playerReference.transform.position.x - controller.transform.position.x;

        direction = newDirection;


        StartCoroutine(ProcessAttackJump());
    }
    IEnumerator ProcessJump()
    {
        //HERE WE JUMP AND THEN WE WAIT. BY THE END OF THIS WE CALL THE THING AGAIN.
        Jump();
        RotateSprite();
        for (float i = 0; i < currentJumpCooldown; i+= 0.1f)
        {
            //WHILE WE ARE WAITING, WE CAN STILL TURN TO THE PLAYER.

            yield return new WaitForSeconds(0.1f);
        }
        //IN THE END WE CALL THE JUMP AGAIN.
        HandleMovement();

    }

    IEnumerator ProcessAttackJump()
    {
        
        yield return new WaitForSeconds(0.2f);
        Debug.Log("we attacked");
        //THEN WE JUMP REALLY HIGH.

        float yPositionBeforeJump = controller.transform.position.y;

        Jump();

        //I WANT HIM TO GET HEAVIER WHILE IN THE AIR.
        
        
       
  

        for (float i = 0; i < currentJumpCooldown; i += 0.1f)
        {
            //WHILE WE ARE WAITING, WE CAN STILL TURN TO THE PLAYER.
            if (yPositionBeforeJump + 4 <= controller.transform.position.y)
            {
                controller.rb.gravityScale = 5;
            }



            yield return new WaitForSeconds(0.1f);
        }
        //IN THE END WE CALL THE JUMP AGAIN.
        controller.rb.gravityScale = 1;
        HandleMovement();
    }

    void Jump()
    {
        //JUMP TOWARDS THE DIRECTION.
        controller.rb.AddForce(new Vector2(direction / 2, currentJumpHeight), ForceMode2D.Impulse);
    }
    void JumpAttack()
    {
        //JUMP TOWARDS TEH PLAYER.
    }


    public void ChangeState(JumperStates newState)
    {
        currentStates = newState;
    }

    void RotateSprite()
    {
        controller.body.transform.localScale = new Vector3(1 * direction, 1, 1);
    }
}
