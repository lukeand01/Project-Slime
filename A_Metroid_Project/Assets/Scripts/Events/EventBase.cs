using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBase : MonoBehaviour, ISaveable
{
    //WE WANT TO SAVE IF ITS ENABLED OR NOT.

    public int id; //THE ID THAT IT HOLDS.
    public int targetId; //THE ID THAT IT AFFECTS.
    public bool started = false;
    public bool completed = false;

    private void Start()
    {
        Observer.instance.EventEventProced += HandleEventInfo;
    }

    public virtual void HandleEventInfo(int _id)
    {
 
        if (_id != id) return;


    }

    public virtual void HandleEvent(PlayerHandler player)
    {
        if (!started)
        {
            Commence(player);
            return;
        }
        if (started)
        {
            Return(player);
            return;
        }
    }


    //EVENTS CAN DO MANY THINGS.
    public virtual void Commence(PlayerHandler player)
    {
        //THIS IS ALWAYS CALLED IN THE FIRST TIME YOU PASS BY IT.

    }

    public virtual void Return(PlayerHandler player)
    {
        //WHAT HAPPENS AFTER YOU PASS IT A SECOND TIME.


    }



    #region HELPFUNCTIONS

    
    public void MoveCharacter(PlayerHandler handler, int direction, float timer)
    {
        handler.ChangePermission(InputPermission.Blocked);
        StartCoroutine(MoveProcess(handler, direction, timer));     
    }
    IEnumerator MoveProcess(PlayerHandler handler, int direction, float timer)
    {
        for (float i = 0; i < timer;)
        {
            handler.movement.Move(direction);
            i += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        handler.ChangePermission(InputPermission.Free);
        yield return null;
    }



    //NEED A FUNCTION TO STOP AND PLAY AN ANIMATOR.

    #endregion

    #region SAVE SYSTEM
    public object CaptureState()
    {
        //WE NEED TO CHECK TWO THINGS:
        //IF ITS ENABLED.
        //IF ITS DESTROYED. HOW CAN WE CHECK IF ITS DESTROYED.
        return new SaveData
        {
            IsEnabled = enabled
        };

    }
    public void RestoreState(object state)
    {
        var savedata = (SaveData)state;

        enabled = savedata.IsEnabled;

    }

    [System.Serializable]
    public struct SaveData
    {
        public bool IsEnabled;
    }

    #endregion
}
