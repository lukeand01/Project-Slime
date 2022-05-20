using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour, ISaveable
{
    //THIS IS SO WE CAN CENTRALIZE EVERYTHING.

    public static PlayerHandler instance;
    public int currentScene;


    #region COMPONENTS
    [HideInInspector] public PlayerMovement movement;
    [HideInInspector] public PlayerInput input;
    [HideInInspector] public PlayerCombat combat;

    [HideInInspector] public Rigidbody2D rb;
    #endregion

    public IInteractable currentInteract;

    public InputPermission currentPermission;

    List<Abilities> abilityList = new List<Abilities>();


    [SerializeField] LayerMask ground;
    BoxCollider2D myCollider;
    private void Awake()
    {

        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        

        DontDestroyOnLoad(gameObject);
        

        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();

    }
    private void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        input.UIInput();
        if (currentPermission == InputPermission.Blocked) return;
        //MIGHT HAVE CONDITIONS HERE.
        input.MovementInput(this, movement);
        input.AttackInput(combat);
        input.InteractInput(this);
        
    }

    public void ChangePermission(InputPermission permission)
    {
        currentPermission = permission;
    }

    //COLLISION
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //IF WE 

        if (collision.GetComponent<EventBase>() != null)
        {
            //I SHOULD HANDLE ALL EVENTS PRESENT IN THE FELLA.

            EventBase[] events = collision.GetComponents<EventBase>();

            for (int i = 0; i < events.Length; i++)
            {
               if (!events[i].enabled) continue;
                events[i].HandleEvent(this);
            }
            //collision.GetComponent<EventBase>().HandleEvent(this);
        }

        if(collision.GetComponent<IInteractable>() != null)
        {
            collision.GetComponent<IInteractable>().InteractUI(true);
            currentInteract = collision.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.GetComponent<IInteractable>() != null)
        {
            collision.GetComponent<IInteractable>().InteractUI(false);
            currentInteract = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //IF THIS EVER COLLIDES WITH AN ENEMY IT TAKES DAMAGE.
        if(collision.gameObject.layer == 8) //THE LAYER 8 BELONGS TO ENEMY.
        {
            //THIS CAN BE TRAPS. BULLETS OR ENEMIES.
            if(collision.gameObject.GetComponent<StateController>() != null)
            {
                //THIS MEANS ITS AN ACTUAL ENEMY.
                //DAMAGE NEEDS TO BE RESERVED TO ATTACKS.
                combat.LoseHealth(this, 10, collision.gameObject);
            }



        }
    }


    private void OnLevelWasLoaded(int level)
    {

        if (level == 0) 
        {

            return;
        }
       


        currentScene = level; 

    }
    public bool IsGrounded(bool jumpCooldown)
    {
        //MAYBE USE COLLIDER?
        if (jumpCooldown)
        {
        
            return false;
        }

        RaycastHit2D ray = Physics2D.Raycast(myCollider.bounds.center, Vector2.down, myCollider.bounds.extents.y + 0.1f, ground);

        return ray.collider != null;
    }

    public bool WallAhead(int direction)
    {

        RaycastHit2D ray = Physics2D.Raycast(myCollider.bounds.center, new Vector3(1 * direction, 0, 0), myCollider.bounds.extents.y + 0.2f, ground);

        



        return ray.collider != null;
    }


    #region SAVE SYSTEM
    public object CaptureState()
    {
        //FOR THE PLAYER
        //WE SAVE HEALTH, AMMO, ABILITIES, POSITION  



        return new SaveData
        {

            positionX = transform.position.x,
            positionY = transform.position.y,

            currentHealth = combat.currentHealth,
            maxHealth = combat.maxHealth,

            stoneAmmo = combat.stoneQuantity,
            webAmmo = combat.webQuantity,

            abilityList = abilityList,

            currentScene = currentScene


        };

        
    }

    public void RestoreState(object state)
    {

        var savedata = (SaveData)state;

        transform.position = new Vector2(savedata.positionX, savedata.positionY);

        combat.currentHealth = savedata.currentHealth;
        combat.maxHealth = savedata.maxHealth;

        combat.stoneQuantity = savedata.stoneAmmo;
        combat.webQuantity = savedata.webAmmo;

        abilityList = savedata.abilityList;

        currentScene = savedata.currentScene;


    }

    //WHAT ABOUT THE JUMP?
    [System.Serializable]
    public struct SaveData
    {
        public float positionX;
        public float positionY;
        public float currentHealth;
        public float maxHealth;

        public int stoneAmmo;
        public int webAmmo;

        public List<Abilities> abilityList;

        public int currentScene;
    }


    #endregion
}


//I SHOULD MAKE A LIST OF THE ALLOWED SKILLS.


//SHOULD I USE THREE BUTTONS FOR THE THREE PROJECTILS? DONT THINK SO.
//ONE BUTTON FOR CHANGING AND OTHER BUTTON FOR SHOOTING THE CHOSEN.
//CLIMBING IS SIMPLY WALKING TO A CLIMBABLE PLACE.
//MELEE IS A THIRD BUTTON/
//TORCH IS AUTOMATIC.
//



public enum InputPermission
{
    Free,
    Partial,
    Blocked

}
public enum Abilities
{
    ShootStone,
    ShootWeb,
    Climb,
    Melee,
    Torch,
    RegenHealth
}


//WHAT HAPPENS IF THE PLAYER IS THROWN DEAD TO A SAVE SPOT.
//WHAT HAPPENS IF HE IS EVER THROWN TO A SAFE SPOT.
//LATER ON YOU WILL NEED TO INPUT THE SAFE SPOT.

//I ALSO NEED TO SAVE THE DIFFERENT INPUTS THE PLAYER HAS CHOSEN.