using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalHandler : MonoBehaviour
{
    public void HandleTimeline(bool choice)
    {     
        Camera.main.GetComponent<CameraFollow>().enabled = choice;

        PlayerHandler playerScript = PlayerHandler.instance;

        if (choice)
        {
            playerScript.ChangePermission(InputPermission.Free);
        }
        if (!choice)
        {
            playerScript.ChangePermission(InputPermission.Blocked);
        }

        //I CALL AN EVENT.
        Observer.instance.OnTimeline(choice); //ITS OPPOSITE. FALSE IT MEANS ITS RUNNING. TRUE MEANS ITS NOT RUNNING.
        

    }


}
