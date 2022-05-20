using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour, IDamageable, ISaveable
{
    [Header("ESSENTIAL")]
    public State currentState;
    SpriteRenderer rend;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public EnemyCombat enemyCombat;
    [HideInInspector] public GameObject playerReference;
    [HideInInspector] public Animator anim;

    [Header("STATS")]
    public Enemy enemyBase;
    float currentHealth;

    #region BOOLS
    [HideInInspector] public bool die;
    bool spawnedAir;
    bool hit;
    [HideInInspector] public bool attacking; //I DONT KNOW IF HIT AND ATTACKING SHOULD BE ONE.
    [HideInInspector] public bool combat;
    [Header("BOOL")]
    #endregion
    #region BODY PARTS

    //I DONT NEED EYES. JUST CHECK HOW CLOSE IT IS.
    [Header("BODY PARTS")]
    [SerializeField] GameObject eyes;
    [SerializeField] public GameObject body;

    #endregion
    #region POSITIONS

   [HideInInspector] public Vector2 originalPosition;
    #endregion

    [Header("MISC")]
    [SerializeField] LayerMask ground;
    [HideInInspector] public int direction = 1;
    [SerializeField] Sprite deadSprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        currentHealth = enemyBase.maxHealth;

        originalPosition = gameObject.transform.position;
        anim = transform.GetChild(0).GetComponent<Animator>();
        enemyCombat = GetComponent<EnemyCombat>();
        rend = body.GetComponent<SpriteRenderer>();

    }
    private void Start()
    {

       

        if (PlayerHandler.instance == null) return;
        playerReference = PlayerHandler.instance.gameObject;
    }
 

    //LETS IMPROVE SOME THINGS

    private void Update()
    {

        if (die) return;

        if (attacking) return;

        if (spawnedAir)
        {
            if (IsGrounded())
            {
                spawnedAir = false;
                originalPosition = gameObject.transform.position;
            }

            return;
        }

        if (hit) 
        {
            Debug.Log("got here");
            return;
        } 


        currentState.UpdateState(this);
    }

    public void TransitionToState(State nextState)
    {
        if(nextState != currentState)
        {
            currentState = nextState;
            for (int i = 0; i < currentState.actions.Length; i++)
            {
                //THIS JUST TELLS THE 
                currentState.actions[i].Begin(this);
            }
        }
    }


    public Vector3 RandomNavSphere(float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }

    #region MODES
    //THIS UNITY SHOULD BE ABLE TO INSTANTLY BE PUT IN COMBAT.
    //THIS UNITY SHOULD HAVE A MODE THAT ONLY STARTS WORKING AFTER LANDING ON THE GROUND.

    public void CombatMode()
    {

    }

    public void SpawnedInAir() => spawnedAir = true;





    #endregion

    #region MOVEMENT
    public void Move(float modifierSpeed)
    {
        gameObject.transform.Translate(Vector3.right * direction *  modifierSpeed * Time.deltaTime);
        if (direction != 0) RotateSprite();

    }
    public void RotateSprite()
    {
        body.transform.localScale = new Vector3(1 * direction, 1, 1);

        if(direction == 1)
        {
            eyes.transform.localRotation = new Quaternion(0, 0, 0, 0);
        }
        if(direction == -1)
        {
            eyes.transform.localRotation = new Quaternion(0, 180, 0, 0);
        }


    }




    #endregion

    #region COMBAT

    //NEED TO PUT THOSE THINGS IN ENEMY COMBAT.
    public void ReceiveEmptyDamage(float damage)
    {
        combat = true;
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    public void ReceiveDamage(float damage)
    {
        hit = true;
        StartCoroutine(ProcessDamage());
        anim.SetBool("TakeHit", true);
        combat = true;
        //ALSO IF THEY TAKE DAMAGE THEY SHOULD INSTANTLY ACTIVATE INTO COMBAT.
        Debug.Log("received damage " + damage);
        currentHealth -= damage;

        if (currentHealth <= 0) Die();
    }

    IEnumerator ProcessDamage()
    {
        //THIS JUST COUNTS TO FREE THE THING FROM MOVING.
        //THE COUNT SHOULD BE BASED.
        Color originalColor = rend.color;

        rend.color = Color.black;
        yield return new WaitForSeconds(0.1f);
        rend.color = originalColor;


        yield return new WaitForSeconds(0.4f);
        anim.SetBool("TakeHit", false);
        hit = false;
    }

    void Die()
    {
        Debug.Log("this was called");

        die = true;
        if(GetComponent<EnemyCombat>() != null)
        {
            GetComponent<EnemyCombat>().Die();
        }

        if(GetComponent<EnemyTrigger>() != null)
        {
            GetComponent<EnemyTrigger>().UseTrigger();
        }

       // body.GetComponent<BoxCollider2D>().isTrigger = true;
        body.gameObject.layer = 12; //IT CANNOT BE HIT OR ATTACKED.
        
        anim.Play("Die");
        anim.enabled = false;
        rend.sprite = deadSprite;

        if (GetComponent<EnemyDropper>() != null) return;

        StartCoroutine(ProcessDeath());


    }

    IEnumerator ProcessDeath()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.GetComponent<ProjectilBase>() != null)
        {
            collision.gameObject.GetComponent<ProjectilBase>().TriggerProjectil(this);
        }

    }
    public bool ShouldTurn()
    {
        //LEDGE.
        //WALL
        //TOO FAR FROM ORIGINAL POINT.
        if (LedgeOrWall()) 
        {
            return true;
        } 


        //TOO FAR FORM ORIGINAL POINT
        if (Vector2.Distance(transform.position, originalPosition) > enemyBase.patrolRange)
        {
            if (body.transform.localScale == new Vector3(1, 1, 1) && direction == 1)
            {
                return true;
            }

            if (body.transform.localScale == new Vector3(-1, 1, 1) && direction == -1)
            {
                return true;
            }

        }


        return false;

    }
    public bool LedgeOrWall()
    {
        //IF THERE IS A WALL    
        RaycastHit2D wallInfoRight = Physics2D.Raycast(eyes.transform.position, Vector2.right, 0.1f, ground);
        RaycastHit2D wallInfoLeft = Physics2D.Raycast(eyes.transform.position, Vector2.left, 0.1f, ground);

        RaycastHit2D ledgeInfo = Physics2D.Raycast(eyes.transform.position, Vector2.down, 5, ground);


        if (wallInfoRight.collider == true && body.transform.localScale == new Vector3(1, 1, 1))
        {
            //THAT MEANS THERE IS A WALL
            return true;
            
        }
        if (wallInfoLeft.collider == true && body.transform.localScale == new Vector3(-1, 1, 1))
        {
            return true;
            
        }

        if(ledgeInfo.collider == false)
        {           
            return true;
        }
        
        return false;

    }

    public bool IsGrounded()
    {
        //MAYBE USE COLLIDER?


        BoxCollider2D collider = transform.GetChild(0).GetComponent<BoxCollider2D>();

        RaycastHit2D ray = Physics2D.Raycast(collider.bounds.center, Vector2.down, collider.bounds.extents.y + 0.1f, ground);

        return ray.collider != null;


    }



    #region SAVING SYSTEM
    public object CaptureState()
    {
        //THE THINGS I WANT TO SAVE FROM STATE?
        //JUST HEALTH FOR NOW.
        //MAYBE I NEED TO SAVE POSITION AS WELL.

        return new SaveData
        {
            savedHealth = currentHealth,
            positionX = transform.position.x,
            positionY = transform.position.y
                     
        };

    }

    public void RestoreState(object state)
    {
        //WHEN I RESTORE AN ENEMY WE INSTANTLY CHECK IF ITS HEALTH IS 0. IF IT IS THEN WE IMMEDIATLY DELETE THE CHAMP.
        var savedata = (SaveData)state;
        currentHealth = savedata.savedHealth;
        if (currentHealth <= 0) Destroy(gameObject);
        transform.position = new Vector2(savedata.positionX, savedata.positionY);
    }
    [System.Serializable]
    public struct SaveData
    {
        public float savedHealth;

        public float positionX;
        public float positionY;
    }
    #endregion
}
