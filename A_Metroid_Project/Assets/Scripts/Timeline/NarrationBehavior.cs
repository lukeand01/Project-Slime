using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class NarrationBehavior : PlayableBehaviour
{
    [TextArea]
    public string narrationText;
    public string narrationName;
    public Sprite narrationSprite;
    public bool narrationEnd;


    bool alreadyCalledTyper = false;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        //HOW TO ACHIEVE THE WRITTER EFFECT HERE?
        //THIS IS CALLED IN EVERYFRAME THAT THE THING IS PLAYING.
        //
        TextMeshProUGUI textGui = playerData as TextMeshProUGUI;

        if (alreadyCalledTyper) return;

        if(DialogueHandler.instance == null)
        {
            //THIS IS SIMPLY FOR TESTING WITHOUT HAVING TO PLAY THE GAME.
            textGui.text = narrationText;
            alreadyCalledTyper = true;
            return;
        }


        DialogueHandler.instance.StartTimelineNarration(textGui, narrationText, narrationName, narrationSprite, narrationEnd);
        alreadyCalledTyper = true;
    }
    
    
}
