using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //IT SHOULD TRY TO BE SMOOTHER.
    //THE CAMERA FOLLOWS FASTER WHEN YOU FALL AND GOES SMOOTHLY WHEN YOU GO UP.
    //THE CAMERA CANT SHOW WHAT THE PLAYER SHOULDNT SEE. LIKE BEHIND A DOOR.
    //WHAT ABOUT GAME BOUNDS? HOW DO I SET THEM UP.

    Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }






}
