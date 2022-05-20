using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class NarrationClip : PlayableAsset
{
    [TextArea]
    public string narrationText;
    public string narrationName;
    public Sprite narrationSprite;
    public bool narrationEnd;
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<NarrationBehavior>.Create(graph);


        NarrationBehavior narrationBehavior = playable.GetBehaviour();
        narrationBehavior.narrationText = narrationText;
        narrationBehavior.narrationName = narrationName;
        narrationBehavior.narrationSprite = narrationSprite;
        narrationBehavior.narrationEnd = narrationEnd;
        return playable;

    }

}
