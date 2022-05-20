using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD.MinMaxSlider;

public class EventSpawner : EventBase
{
    //THIS WILL USE SPAWNER TO SPAWN THINGS. 
    //THEY COULDNT BE SPAWNED RANDOMLY, OR SET EARLY.
    //THE CHOICE CREATURE ARE NOT RANDOM, BUT THE ORDER THEY ARE IS RANDOM.

    [Header("BOOLS")]
    [Tooltip("This means that the number of spawns being used changes every cycle")]
    [SerializeField] bool randomTogether; //THIS MEANS THAT THE NUMBER OF SPAWN BEING SUED TOGETHER CHANGES EVERY TIME.
    [SerializeField] bool instantActivation; //THIS MEAN WE CALL IT IN START.


    [Header("NUMBERS")]
    [Tooltip("This represents the number of spawns that should be used per cycle.")]
    [SerializeField] int spawnPlacesUsedTogether; //THIS MEANS THE NUMBER OF SPAWNS THAT SHOULD SPAWN AT ONCE.
    [SerializeField] float timeBetweenSpawns; //TIME BETWEEN SPAWNS.
    [Tooltip("The number of cycles till the spawning stops")]
    [SerializeField] int spawnRounds; 
    int currentToSpawn;


    [Header("LISTS")]
    [SerializeField] List<Transform> spawnPointsList = new List<Transform>();
    [SerializeField] GameObject[] enemyList;

    EnemyTrigger trigger;


    [Header("KEEP TRACK OF END")]
    List<StateController> controllerList = new List<StateController>();


    private void OnEnable()
    {
        foreach (Transform child in transform)
        {
            //child is your child transform
            spawnPointsList.Add(child);
        }
    }
    private void Start()
    {
        //currentToSpawn = spawnNumber;
        Debug.Log("this is the strt of spawner");

        spawnPlacesUsedTogether = Mathf.Clamp(spawnPlacesUsedTogether, 0, spawnPointsList.Count); ///THIS IS JUST IN CASE I FUCK UP.

        if(GetComponent<EnemyTrigger>() != null)
        {
            trigger = GetComponent<EnemyTrigger>();
        }

        if (instantActivation)
        {
            Debug.Log("instant");
            StartCoroutine(SpawnProcess());
        }

    }

    private void Update()
    {
        if (trigger == null) return; //IF THERE IS NO TRIGGER THERE IS NOTHING TO WORRY ABOUT.
        HandleTrigger();
    }

    void HandleTrigger()
    {

        if(currentToSpawn >= spawnRounds && AllControllersAreDead()) 
        {
            Debug.Log("the trigger should be called");
            trigger.UseTrigger();       
        }
    }


    public override void Commence(PlayerHandler player)
    {
        if (instantActivation) return;
        //I START THE PROCESS.
        StartCoroutine(SpawnProcess());

    }

    IEnumerator SpawnProcess()
    {
        //THREE FACTORS:
            //NUMBER OF TURNS OF RESPAWN THAT WILL HAPPEN.
            //NUMBER OF SPAWN POINTS USED.
            //NUMBER OF TIME BETWEEN SPAWNS.


        for (int i = 0; i < spawnRounds; i++)
        {
            //FOR EACH SPAWN ROUNDS WE CHECK SOME THINGS.
            //
            currentToSpawn += 1;

            List<Transform> spawnPositions = chosenSpawnPositions(spawnPointsList.Count);

           // Debug.Log("this is the number of the list " + spawnPositions.Count);

            for (int y = 0; y < spawnPositions.Count; y++)
            {
                ///FOR EACH SPAWN POSITION WE SPAWN A RANDOM ENEMY FROM THE LIST OF ENEMIES.
                int randomEnemy = Random.Range(0, enemyList.Length - 1);
                Spawn(enemyList[randomEnemy], spawnPositions[y]);
            }

            yield return new WaitForSeconds(timeBetweenSpawns);
        }



    }


    void HereToWait()
    {

        /*
        for (int i = 0; i < spawnNumber; i++)
        {
            currentToSpawn -= 1;
            ///DECIDE WHICH SPAWNSPOSITIONS WILL BE USED.
            List<Transform> newList = new List<Transform>();
            if (randomTogether)
            {
                int newSpawnTogether = 0;
                newSpawnTogether = Random.Range(spawnsTogether, spawnPoints.Count - 1);
                newList = chosenSpawnPositions(newSpawnTogether);
            }
            else
            {
                newList = chosenSpawnPositions(spawnsTogether);
            }



            for (int y = 0; y < newList.Count; y++)
            {

                int randomEnemy = Random.Range(0, enemiesSpawn.Length - 1);
                Spawn(enemiesSpawn[randomEnemy], newList[y]);
            }

            //yield return new WaitForSeconds(timeBetweenSpawns);
        }

        */
    }

    List<Transform> chosenSpawnPositions(int numberOfPlacesallowed)
    {
        //FOR NOW WE JUST RETURN THE LIST ITSELF BECAUSE WE DONT NEED TO BOTHER ABOUT IT.
        Debug.Log("got here once " + numberOfPlacesallowed);

        if(numberOfPlacesallowed == spawnPointsList.Count)
        {
            Debug.Log("just return the same list");
            return spawnPointsList;
        }


        //I WILL CARE ABOUT THIS LATER
        

        List<Transform> copyList = new List<Transform>();
        copyList = spawnPointsList;
        Debug.Log("the copy list is: " + copyList.Count);

        List<Transform> newList = new List<Transform>();


        for (int i = 0; i < numberOfPlacesallowed; i++)
        {         
            int randomChoice = Random.Range(0, copyList.Count - 1);

            Debug.Log("the random choice was: " + randomChoice);

            newList.Add(copyList[randomChoice]);
            copyList.RemoveAt(randomChoice);

        }
        
        return newList;
    }

    void Spawn(GameObject spawn, Transform spawnPlace)
    {             
        GameObject newObject = Instantiate(spawn, spawnPlace.position, Quaternion.identity);
        StateController controller = newObject.GetComponent<StateController>();
        controller.SpawnedInAir();
        controllerList.Add(controller);
    }

    

    bool AllControllersAreDead()
    {
       
        for (int i = 0; i < controllerList.Count; i++)
        {
            if (controllerList[i].die) continue;

            return false;

        }

        return true;
    }
}

