using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    GameObject deathMenuHolder;
    private void Start()
    {
        deathMenuHolder = transform.GetChild(0).gameObject;

        Observer.instance.EventDeathMenu += OpenMenu;
    }

    void OpenMenu(int empty)
    {
        deathMenuHolder.SetActive(true);
    }

    public void Respawn()
    {
        SaveHandler.instance.Load();
        deathMenuHolder.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("should exit the game");
    }
    

}
