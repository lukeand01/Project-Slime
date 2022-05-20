using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    //WE HAVE A LIST OF POSSIBLE BACKGROUNDS.
    //EVERYTIME ONE BACKGROUND LEAVES THE EXTENSIONS OF THE CAMERA, WE PULL THE NEXT BACKGROUND.

    public GameObject[] backgrounds;
   public Camera mainCamera;
    Vector2 screenBounds;

   public GameObject mainBackground;
   public GameObject oldBackground;

    private void Start()
    {
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));


    }


    private void LateUpdate()
    {
        mainBackground.transform.Translate(Vector3.down * 3 * Time.deltaTime);

        if(oldBackground != null)
        {
            oldBackground.transform.Translate(Vector3.down * 3 * Time.deltaTime);
        }
        if(mainBackground.transform.position.y <= -3.50f)
        {
            CreateNewBackground();
        }
    }

    void CreateNewBackground()
    {
        int chance = Random.Range(0, backgrounds.Length);

        GameObject newObject = Instantiate(backgrounds[chance], new Vector3(-0.860000014f, 18.5799999f, -19.4769897f), Quaternion.identity);

        if(oldBackground != null)
        {
            Destroy(oldBackground);
        }

        oldBackground = mainBackground;
        mainBackground = newObject;

    }
}
