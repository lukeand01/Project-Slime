using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerInput : MonoBehaviour
{
    //THIS IS THE ACTUAL INPUT THAT WILL CALL OTHER THINGS.

   

    bool attackCooldown;
    bool jumpCooldown;

    //MAYBE CREATE SOME KIND OF ACTION EVENT.
    //SO WE KNOW WHEN WE PUSH A BUTTTON.

    

   //I MAKE A LIST 
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode lookUp;
    public KeyCode lookDown;
    public KeyCode jump;
    public KeyCode fireProjectil;
    public KeyCode changeProjectil;
    public KeyCode interact;
    public KeyCode menu;
    public KeyCode meleeAttack;
    public KeyCode chargeAbility;

    private void OnEnable()
    {

        
        moveLeft = KeyCode.A;
        moveRight = KeyCode.D;
        lookUp = KeyCode.W;
        lookDown = KeyCode.S;

        jump = KeyCode.Space;

        fireProjectil = KeyCode.P;
        changeProjectil = KeyCode.Q;

        interact = KeyCode.E;

        chargeAbility = KeyCode.LeftShift;
        menu = KeyCode.Escape;
    }

    #region MOVEMENT
    public void MovementInput(PlayerHandler handler, PlayerMovement movement)
    {

        ChargeAbilities();
   
        if (Input.GetKey(moveLeft) && !handler.WallAhead(-1))
        {
            movement.Move(-1);
        }
        if (Input.GetKey(moveRight) && !handler.WallAhead(1))
        {
            movement.Move(1);
        }


        JumpInput(handler, movement);

    }
    bool isJumping;
    [SerializeField] float jumpHeight = 0.5f;
    public float currentJumpHeight;


    void JumpInput(PlayerHandler handler, PlayerMovement movement)
    {
        //THE JUMP WORKS AS FOLLOWING
        //HOW DO I MAKE THE LONGER JUMP?

        if (handler.IsGrounded(jumpCooldown) && handler.rb.gravityScale > 1)
        {
            //THEN WE SEND THE INFO AGAIN.
            Observer.instance.OnCameraJump(false, null);
            handler.rb.gravityScale = 1;
        }

        if (Input.GetKeyUp(jump))
        {
            currentJumpHeight = 0;
            handler.rb.gravityScale = 2;
            isJumping = false;
            
        }


        if (!handler.IsGrounded(jumpCooldown) && isJumping == false) return;


        if (Input.GetKey(lookDown))
        {
            handler.rb.gravityScale = 5;
        }
        if (Input.GetKeyUp(lookDown))
        {
            handler.rb.gravityScale = 2;

        }

        if (Input.GetKey(chargeAbility)) return;


        if (Input.GetKeyDown(jump))
        {

            movement.Jump(1);          
            isJumping = true;
        }

        if (Input.GetKey(jump))
        {         
            
            currentJumpHeight += Time.deltaTime;
            if (currentJumpHeight > jumpHeight)
            {
                isJumping = false;
                currentJumpHeight = 0;
                return;
            }

            movement.Jump(1);
            
        }

        if (Input.GetKeyUp(jump))
        {
            currentJumpHeight = 0;
            handler.rb.gravityScale = 2;
            isJumping = false;

        }


    }
    
    void ChargeAbilities()
    {
        //UNSURE ABOUT THIS CHARGE THING. I WONT HAVE A DASH PERPHAPS.

        if (Input.GetKey(chargeAbility))
        {
            //SHOULD CHARGE DO SOMETHING?

            if (Input.GetKey(jump))
            {

            }
            if (Input.GetKeyUp(jump))
            {
                


            }


        }

        if (Input.GetKeyUp(chargeAbility))
        {
            //WE CANCEL IT


        }


    }


    void RefreshJump() => jumpCooldown = false;
    

    
    #endregion

    #region COMBAT
    public void AttackInput(PlayerCombat combat)
    {
        //THE ATTACK WILL BE SPITTING STONES.

        if (Input.GetKeyDown(changeProjectil))
        {
            combat.ChangeShooter();

        }

        if (attackCooldown) return;
        
        if (Input.GetKeyDown(fireProjectil)) //
        {
            attackCooldown = true;
            Invoke("RefreshAttack", 0.2f);
            HandleProjectil(combat);           
        }

    }

    void RefreshAttack() => attackCooldown = false; //MAYBE SHOW IT VISUALLY?


    void HandleProjectil(PlayerCombat combat)
    {

        combat.HandleShooter(GetDirection());

        
    }

    Vector3 GetDirection()
    {

        if (Input.GetKey(moveLeft))
        {
            return new Vector3(-1, 0, 0);
            
        }
        if (Input.GetKey(moveRight))
        {
            return new Vector3(1, 0, 0);
            
        }
        if (Input.GetKey(lookUp))
        {
            return new Vector3(0, 1, 0);
         
        }

        Vector3 nullDirection = new Vector3(-this.gameObject.transform.GetChild(0).transform.localScale.x, 0, 0);
        return nullDirection;
    }

    #endregion

    #region INTERACT


   public void InteractInput(PlayerHandler handler)
    {
        //WE NEED TO CHECK THE LIST OF 
        if (Input.GetKeyDown(interact) && handler.currentInteract != null)
        {
            if (handler.currentInteract.Interact())
            {
                handler.currentInteract = null;
            }
        }


    }

    #endregion

    #region ABILITIES

    void AbilitiesInput()
    {




    }

    #endregion

    #region UI
    public void UIInput()
    {
        if (Input.GetKeyDown(menu))
        {
            //WE TURN THE MENU ON.
            Observer.instance.OnHandlePause(0);
        }
    }

    #endregion


    

}
