using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using DG.Tweening;

public class DialogueHandler : MonoBehaviour
{
    //IF I HAVE TIME IN THE END MAYBE IMPROVE THIS SCRIPT?
    //THIS HAS VIRTUALLY DOUBLED FUNCTIONS WITH LITTLE DIFFERENCE. FOR NOW, IT WORKS.
    //NOW IT HAS THREE FUNCTIONS THAT DO ALMOST THE SAME THING. IF I HAVE TIME I SHOULD FIX THIS.


    public static DialogueHandler instance;


    [SerializeField] TextAsset test;
    [Header("COMPONENTS")]
    [SerializeField] GameObject holder;
    [SerializeField] Image portrait;
    [SerializeField] TextMeshProUGUI portraitName;
    [SerializeField] TextMeshProUGUI dialogueText;


    [Header("STORY COMPONENTS")]
    Story currentStory;
    bool writting = false;
    bool newWritting;
    float writtingSpeed;
    [SerializeField] float normalWritting;
    [SerializeField] float fastWritting;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (writting)
        {
            //THEN WE CAN ACCELRATE IT.
            if (Input.GetKeyDown(KeyCode.E))
            {
                writtingSpeed = fastWritting;
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                writtingSpeed = normalWritting;
            }
        }
    }


    #region DIALOGUE
    public void StartDialogue(TextAsset newDialogue)
    {
        currentStory = new Story(newDialogue.text);
        //THEN WE RAISE CURTAIN
        StartCoroutine(RaiseCurtains(true, true));
        PlayerHandler.instance.ChangePermission(InputPermission.Blocked);

    }

    


    void ContinueDialogue()
    {

        if (currentStory.canContinue)
        {

            StartCoroutine(DialogueProcess(currentStory.Continue()));

        }
        else
        {
            if (currentStory.currentChoices.Count == 0)
            {
                //THIS IS THE END OF THE COVNERSATION. GIVE THE CURRENT NPC THE RELATION. 
                Debug.Log("the dialogue is over");
                holder.SetActive(false);
                PlayerHandler.instance.ChangePermission(InputPermission.Free);
                return;
            }
            
        }

    }


    IEnumerator DialogueProcess(string newText)
    {
        //INK IS DIFFERENT NEED TO CHECK THAT.

        
        HandleTags(currentStory.currentTags);
        writting = true;
        dialogueText.text = "";
       float writtingSpeed = normalWritting;

        foreach (char c in newText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(writtingSpeed);
        }
        writting = false;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        ContinueDialogue();
        yield return null;
    }

    #region PORTRAITS

    private const string speaker_Tag = "speaker";
    private const string portrait_Tag = "portrait";

    void HandleTags(List<string> tagList)
    {

        foreach (string tag in tagList)
        {
            Debug.Log("there is one tag here " + tag);

            string[] splitTag = tag.Split(':');

            string keyTag = "";
            string tagValue = "";


            keyTag = splitTag[0];
            tagValue = splitTag[1];


            if(keyTag == speaker_Tag)
            {
                HandleTagPortrait(tagValue);
            }

            if(keyTag == portrait_Tag)
            {
                HandleTagSpeaker(tagValue);
            }

        }


    }

    void HandleTagPortrait(string tagValue)
    {
        Sprite portraitSprite = Resources.Load<Sprite>($"CharacterSprites/{tagValue}");

        portrait.sprite = portraitSprite;
    }
    void HandleTagSpeaker(string tagValue)
    {
        portraitName.text = tagValue;


    }

    #endregion
    #endregion

    #region REGULAR NARRATION
    public void StartRegularNarration(string newText)
    {
        //EVERYTIME I RECEIVE A NEW ONE. I SHOULD COMPLETELY IGNORE THE NEXT.

        //I SHOULDNT RAISE THE CURTAIN IF THE CURTAINS ARE ALREADY RAISED.
        //WHAT IF THEY ARE IN THE PROCESS TO LOWER.

        if (writting)
        {
            StopAllCoroutines();
            writting = false;
        }

        StartCoroutine(RaiseCurtains(true, false));       
        StartCoroutine(InGameNarrationProcess(newText));
    }
    IEnumerator InGameNarrationProcess(string newText)
    {
        //THE PROBLEM IS THAT IF THEY GO TOGEHTER THERE WILL BE TWO SIMUL OPERATIONS DOING THE THING IN THE SAME PLACE.
        //
        yield return new WaitForSeconds(0.1f);
        writting = true;
        dialogueText.text = "";
        foreach (char letter in newText)
        {
            dialogueText.text += letter;


            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(1f);
        writting = false;
        StartCoroutine(RaiseCurtains(false, false));
    }
    #endregion

    #region TIMELINE NARRATION
    //NEED TO THINK IF I NEED A BLOCKER HERE.




    public void StartTimelineNarration(TextMeshProUGUI textGui, string newText, string _portraitName, Sprite _portraitSprite, bool narrationEnd)
    {
        //HERE WE TELL THE MAIN CAMERA



        portraitName.text = _portraitName;
        portrait.sprite = _portraitSprite;

        StartCoroutine(RaiseCurtains(true, false));
        StartCoroutine(NarrationProcess(textGui, newText, narrationEnd));
    }
    IEnumerator NarrationProcess(TextMeshProUGUI textGui, string newText, bool narrationEnd)
    {

        textGui.text = "";

        foreach (char letter in newText)
        {
            textGui.text += letter;           
         
            yield return new WaitForSeconds(0.02f);
        }

        if (!narrationEnd) yield break;

        yield return new WaitForSeconds(1.5f);
        StartCoroutine(RaiseCurtains(false, false));
    }
    #endregion


    #region UTILS
    IEnumerator RaiseCurtains(bool present, bool isLong)
    {

        if (present)
        {
            if (holder.activeInHierarchy) yield break;

            dialogueText.text = "";
            holder.SetActive(true);
            holder.transform.DOLocalMoveY(10, 0.5f);
            yield return new WaitForSeconds(0.4f);
        }
        if (!present)
        {
            holder.transform.DOLocalMoveY(-150, 1f);
            yield return new WaitForSeconds(1.1f);
            holder.SetActive(false);
        }

        //MAYBE I NEED TO STOP THE GAME WHEN DIALOGUE STARTS.

        if (isLong) ContinueDialogue();

        if (!isLong)

            yield return null;
    }

    #endregion
}
[System.Serializable]
public class DialogueClass
{
    //
    [TextArea]
    public string _dialogue;
    public string portraitName;
    public Sprite portraitSprite;



}