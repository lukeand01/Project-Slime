using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;


public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;

    //THIS IS RESPONSIBLE FOR CUTSCENES.


    public PlayableDirector director;
    [SerializeField] TimelineAsset entranceAsset;
    [SerializeField] Vector2 playerOriginalPosition;


    private void Awake()
    {

        director = GetComponent<PlayableDirector>();
        instance = this;



        return; ///WONT DO THIS FOR THE MEANTIME BECAUSE IM TESTING STUFF

        if (SaveHandler.instance.FileExists())
        {
            //I NEED TO SAY NOTHING MORE BECAUSE THE DATA IS ALREADY SUPPOSED TO BE THERE.
            SaveHandler.instance.Load();
            return;
        }


        //PlayerHandler.instance.gameObject.transform.position = playerOriginalPosition;
        //PlayNewTimeline(entranceAsset);
    }

    public void PlayNewTimeline(TimelineAsset newTimeline)
    {
        //TELL EVERYONE THAT IT IS RUNNING

        director.playableAsset = newTimeline;
        director.Play();
    }

    #region TIMELINE NARRATION
 


    #endregion
}
