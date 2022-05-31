using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortMusic : MonoBehaviour
{

    public static ShortMusic instance;


    private void Awake()
    {
        instance = this;
    }

    //
    public void CreateSound(AudioClip clip)
    {
        ///WE CREATE A SOUND THAT PLAYS A SOUND. IT DECAYS AND IS DESTROYED.
        GameObject newObject = new GameObject();
        newObject.AddComponent<AudioSource>();
        newObject.AddComponent<MusicUnit>();
        GameObject createdObject = Instantiate(newObject, new Vector3(0, 0, 0), Quaternion.identity);
        createdObject.GetComponent<MusicUnit>().PlayMusic(clip);
    }

}
