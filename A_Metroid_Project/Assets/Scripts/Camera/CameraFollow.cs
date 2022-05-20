using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour, ISaveable
{
    //THE CAMERA IS A BIT FORWARD WHERE THE PLAYER IS TURNED.
    //HANDLE JUMPING. THE CAMERA SHOULDNT FOLLOW THE JUMP. 
    //JUST FOLLOW WHEN THE PLAYER LANDS SOMEWHERE OR GOES TOO FAR FROM THE ORIGINAL SPOT.
    //


    PlayerHandler playerHandler;
    Transform player;
    BoxCollider2D camBox;
    GameObject[] boundaries;
    Bounds[] allBounds;
    Bounds targetBound;

    public float speed;
    public float waitingTime = 0.5f;


    Transform posJumpPlayer;
    bool isJumping;



    private void Awake()
    {      
        FindLimits();
    }

    private void Start()
    {
        playerHandler = PlayerHandler.instance;
        player = PlayerHandler.instance.transform;
        camBox = GetComponent<BoxCollider2D>();

        Observer.instance.EventCameraJump += ReceiveJumpInfo;

        
    }
    private void LateUpdate()
    {
        if(waitingTime > 0)
        {
            waitingTime -= Time.deltaTime;
        }
        else
        {
            SetOneLimit();
            FollowPlayer();
        }

    }


    void FindLimits() //FINDS ALL LIMITS ON THE CURRENT SCENE.
    {
        boundaries = GameObject.FindGameObjectsWithTag("Boundary");
        allBounds = new Bounds[boundaries.Length];
        for (int i = 0; i < allBounds.Length; i++)
        {
            allBounds[i] = boundaries[i].gameObject.GetComponent<BoxCollider2D>().bounds;
        }

    }

    void SetOneLimit() //SETS THE CAMERA LIMIT BASED ON WHERE THE PLAYER IS.
    {
        for (int i = 0; i < allBounds.Length; i++)
        {
            if(player.position.x > allBounds[i].min.x && player.position.x < allBounds[i].max.x && player.position.y > allBounds[i].min.y && player.position.y < allBounds[i].max.y)
            {
                targetBound = allBounds[i];
                
                return;
            }
        }

        Debug.LogError("My error: something went wrong in setting the current camera");
    }

    public float distanceSight;
    void FollowPlayer()
    {
        //IF MOVES IN Y. WE DONT FOLLOW.
        //IF MOVES IN X WE FOLLOW.
        //IF WE GO FAR IN Y. WE FOLLOW.
        //IF GROUNDED. WE FOLLOW.

        //I AM USING PHYSIC FOR JUMP BUT NOT FOR MOVEMENT.


        //IF I JUMP I DONT MOVE IT IN TEH Y.



        float xTarget = GetTarget(true);
        float yTarget = GetTarget(false);
        float playerFacingDirection = player.transform.GetChild(0).localScale.x * distanceSight;



        Vector3 target = new Vector3(xTarget - playerFacingDirection, yTarget, transform.position.z);

        if (!ShouldCameraFollow())
        {
            target = new Vector3(xTarget - playerFacingDirection, transform.position.y, transform.position.z);
        }
        


        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
    }

    float GetTarget(bool axisX)
    {
        float newFloat = 0;

        //I NEED TO AFFECT THE X BASED IN WHERE THE PLAYER IS TURNED.
        if (axisX)
        {
            newFloat = camBox.size.x < targetBound.size.x ? 
                Mathf.Clamp(player.position.x, targetBound.min.x + camBox.size.x / 2, targetBound.max.x - camBox.size.x / 2) :
                (targetBound.min.x + targetBound.max.x) / 2; ;
        }
        if (!axisX)
        {
            newFloat = camBox.size.y < targetBound.size.y ?
                Mathf.Clamp(player.position.y, targetBound.min.y + camBox.size.y / 2, targetBound.max.y - camBox.size.y / 2) :
                (targetBound.min.y + targetBound.max.y) / 2;
        }
        return newFloat;
    }


    bool ShouldCameraFollow()
    {
        if (isJumping)
        {
            if(Vector2.Distance(player.position, posJumpPlayer.position) > 10)
            {
                //THIS MEANS THAT THE PLAYER IS TOO FAR AND WE SHOULD FOLLOW HIM.
                return true;
            }


            //OTHERWISE WE SHOULDNT
            return false;


        }


        return true;
    }

    void ReceiveJumpInfo(bool _isJumping, Transform pos)
    {
        //WE CHECK HOW FAR IT WENT.
        //

        isJumping = _isJumping;

        if (_isJumping)
        {
            posJumpPlayer = pos;
        }
        if (!_isJumping)
        {
            //THEN I DONT CARE ABOUT POSITION.
            //BECAUSE THIS MEANS HE HAS LANDED SOMEWHERE.


        }
       

    }


    #region SAVE SYSTEM
    public object CaptureState()
    {
        return new SaveData
        {
            positionX = transform.position.x,
            positionY = transform.position.y,
        };
    }

    public void RestoreState(object state)
    {
        var savedata = (SaveData)state;

        transform.position = new Vector3(savedata.positionX, savedata.positionY, -20);

    }

    [System.Serializable]
    public struct SaveData
    {
        public float positionX;
        public float positionY;
        
    }

    #endregion
}

