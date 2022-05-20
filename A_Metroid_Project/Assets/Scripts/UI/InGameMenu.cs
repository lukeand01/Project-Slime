using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InGameMenu : MonoBehaviour
{

    
    [SerializeField] GameObject menu;

    [Header("EXIT")]
    [SerializeField] GameObject exitHolder;
    [SerializeField] TextMeshProUGUI lastSaveReport;

    GameObject currentUI;

    private void Start()
    {
        Observer.instance.EventHandlePause += HandleMenu;
    }

    void HandleMenu(int empty)
    {
        if (menu.activeInHierarchy)
        {
            menu.SetActive(false);
            Time.timeScale = 1;
            return;
        }
        if (!menu.activeInHierarchy)
        {
            menu.SetActive(true);
            Time.timeScale = 0;
            return;
        }
    }

    public void HandleUI(GameObject newUI)
    {
        //WE OPEN OTHER UI.
        if(currentUI != null)
        {
            currentUI.SetActive(false);
        }

        newUI.SetActive(true);
        currentUI = newUI;

    }

    public void ContinueGame()
    {
        menu.SetActive(false);
    }
    public void Settings()
    {
        //OPEN SETTINGS.
    }

    #region EXIT HANDLING
    
    public void ExitMenu()
    {
        //RETURN TO THE ORIGINAL SCENE.
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(PlayerHandler.instance.currentScene);
        SceneManager.LoadSceneAsync("MainMenuScene");
    }
    public void ExitDesktop()
    {     
        Application.Quit();
    }
    public void CancelExit()
    {
        exitHolder.SetActive(false);
    }
    #endregion
}
