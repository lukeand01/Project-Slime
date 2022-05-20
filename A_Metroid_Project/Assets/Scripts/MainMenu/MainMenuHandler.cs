using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class MainMenuHandler : MonoBehaviour
{
    //THIS SHOULD HANDLE TEH TRANSTION.
    //THIS SHOULD CREATE THE CONTINUE SLOT ONLY IF THERE IS A SAVE.
    [Header("HOLDERS")]
    [SerializeField] GameObject mainMenuHolder;
    [SerializeField] GameObject playHolder;
    [SerializeField] GameObject settingsHolder;


    [Header("ESPECIFIC")]
    [SerializeField] GameObject continueButton;


    GameObject currentUI;

    private void Start()
    {


        //MAYBE I SHOULD HOLD ORIGINAL INFORMATION HERE.
        //WHEN I CLICK TO START A NEW GAME I GIVE ALL THE INFO TO THE PLAYER.

        currentUI = mainMenuHolder;


        if (SaveHandler.instance.FileExists())
        {
            SaveHandler.instance.Load();
            CreateContinueButton();
            return;
        }
        continueButton.SetActive(false);


    }


    public void HandleNewUI(GameObject targetUI)
    {
        if (currentUI != null) currentUI.SetActive(false);

        targetUI.SetActive(true);
        currentUI = targetUI;
    }

    void CreateContinueButton()
    {
        continueButton.SetActive(true);

        GameObject statsHolder = continueButton.transform.GetChild(1).gameObject;


        TextMeshProUGUI maxHealth = statsHolder.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI currentHealth = statsHolder.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        maxHealth.text = $"Max Health: {PlayerHandler.instance.combat.maxHealth}";
        currentHealth.text = $"Current Health: {PlayerHandler.instance.combat.currentHealth}";

    }

    public void ContinueGame()
    {
        //JUST LOAD THE PLAYER INTO THE RIGHT SCENE.
        //ANOTHER MANAGE WILL PUT IT IN THE RIGHT POS AND WITH THE RIGHT DATA.
        SceneManager.UnloadSceneAsync(0);
        SceneManager.LoadSceneAsync(PlayerHandler.instance.currentScene);
        
    }

    public void StartNewGame()
    {
        //DELETE THE CURRENT SAVE IF THERE IS ONE.
        SceneManager.UnloadSceneAsync(0);
        SaveHandler.instance.DeleteFile();
        SceneManager.LoadSceneAsync(PlayerHandler.instance.currentScene); 


    }


}
