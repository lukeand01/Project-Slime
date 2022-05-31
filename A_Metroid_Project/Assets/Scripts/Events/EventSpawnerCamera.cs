using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpawnerCamera : MonoBehaviour
{
    ///THIS SPAWNS AN ENEMY EVERYTIME IT CAN(COOLDOWN ADN ENEMY LIMIT) AND ONLY WHILE IT IS NOT UNDER THE CAMERA
    ///IMPORTANT: I DONT CARE ABOUT THIS DATA. BUT THE ENEMIES SHOULD BE DESTROYED.



    bool underCamera;



    [SerializeField] float cooldown;
    float currentCooldown;
    [SerializeField] List<GameObject> enemyList = new List<GameObject>();
    [SerializeField] int maxAllowedEnemy;
    [SerializeField] List<StateController> aliveEnemy = new List<StateController>();

    private void Awake()
    {
        Observer.instance.EventEnemyDie += HandleTrackList;
    }

    private void Update()
    {
        if (underCamera) return;
        if (aliveEnemy.Count >= maxAllowedEnemy) return;

        if(currentCooldown < cooldown)
        {
            currentCooldown += Time.deltaTime;
            return;
        }

        currentCooldown = 0;
        Spawn();

    }

    void Spawn()
    {
        //WE SPAWN RANDOM FROM A SELECTION.
        //WE ALSO HAVE TO KEEP TRACK OF THOSE SPAWNED, BECAUSE YOU CAN ONLY SPAWN MORE WHEN THAT IS DEAD.

        int choice = Random.Range(0, enemyList.Count - 1);

        GameObject newObject = Instantiate(enemyList[choice], transform.position, Quaternion.identity);
        newObject.GetComponent<StateController>().shouldBeDestroyed = true;
        aliveEnemy.Add(newObject.GetComponent<StateController>());
    }

   void HandleTrackList(string guid)
    {
        for (int i = 0; i < aliveEnemy.Count; i++)
        {
            if(aliveEnemy[i].guid == guid)
            {
                aliveEnemy.RemoveAt(i); 
            }
        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("a trigger entered " + collision.gameObject.name);

        if(collision.tag == "MainCamera")
        {
            Debug.Log("and this was the camera");
            underCamera = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("a trigger left " + collision.gameObject.name);

        if (collision.tag == "MainCamera")
        {
            Debug.Log("camera left");
            underCamera = false;
        }
    }

    
}
