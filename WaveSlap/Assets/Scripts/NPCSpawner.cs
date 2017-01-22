using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCSpawner : MonoBehaviour {

    public int numOfNPC;
    public int maxNumOfNPC = 40;

    public int numOfNPCAtStart = 3;

    public int numOfSpawnAtOnce = 1;
    public int maxNumOfSpawnAtOnce = 4;

    public float minX = -8.18f;//-12.5f
    public float maxX = 8.18f;//12.5f
    public float minY = -5.17f;//-3.9f
    public float maxY = 5.17f;//3.9f

    public List<GameObject> prefabNPC_List;
    public GameObject prefabSlapper;

    //Event Manager
    public EventManager em;//MUST BE SET IN INSPECTOR

    // Use this for initialization
    void Start () {
        em = GameObject.Find("EventManager").GetComponent<EventManager>();

        spawnInitialHorde();
        spawnWaver();
    }
	
	// Update is called once per frame
	void Update () {

        //DEBUG
        if (Input.GetKeyDown(KeyCode.R))
        {
            spawnNumOfNPC(2);
        }
    }

    //   $$$$$$$$$$$$$$$$$$$
    //   $$$              $$
    //   $$$  SPAWN CODE  $$
    //  $$$$              $$$
    //$$$$$$$$$$$$$$$$$$$$$$$$$
    //
    //@
    public void spawnInitialHorde()
    {
        Vector3 spawnVector = new Vector3();
        int prefabIndex = 0;

        if (numOfNPCAtStart > maxNumOfNPC)
        {
            Debug.Log("numOfNPCAtStart > maxNumOfNPC");
            return;
        }

        for (int i = 0; i < numOfNPCAtStart; i++)
        {
            spawnVector = getSpawnVector3();

            prefabIndex = pickRandomSpritePrefab();

            GameObject npcCopy = (GameObject)Instantiate(prefabNPC_List[prefabIndex]);

            npcCopy.transform.position = spawnVector;
        }

        //Tell the EventManager that the initial horde was spawned
        //So the GlobalHappyController can gather all of the starting NPCs
        //And begin normal functioning
        em.EM_InitialHorde_Spawned();
    }
    //@
    //@@
    public void spawnWaver()
    {
        Vector3 spawnVector = getSpawnVector3();
        int prefabIndex = pickRandomSpritePrefab();

        GameObject waver = (GameObject)Instantiate(prefabNPC_List[prefabIndex]);

        waver.GetComponent<NPCMovementControl>().enabled = false;
        waver.AddComponent<WaverMovement>();

        waver.tag = "Player";

        waver.GetComponent<WaveController>().isPlayer = true;
        waver.GetComponent<HappyController>().isPlayer = true;

        waver.transform.position = spawnVector;

        //tell the EventManager that we have spawned the Waver
        //so that the GlobalHappyController can add it to its list
        em.EM_Waver_Spawned();

        //GameObject.Find("GlobalHappinessController").GetComponent<GlobalHappyController>().addNPC(waver);
    }
    public void spawnSlapper()
    {

    }
    //@@
    public void spawnNPC()
    {
        if (numOfSpacesLeft() < 1) { return; }

        Vector3 spawnVector = getSpawnVector3();
        
        int prefabIndex = pickRandomSpritePrefab();
        GameObject npcCopy = (GameObject)Instantiate(prefabNPC_List[prefabIndex]);
        //GameObject npcCopy = (GameObject)Instantiate(prefabNPC);

        npcCopy.transform.position = spawnVector;

        //Call event from EventManager
        em.EM_NPC_Spawned(npcCopy);

    }
    public void spawnNPC(GameObject npcCopy)
    {
        if (numOfSpacesLeft() < 1) { return; }

        Vector3 spawnVector = getSpawnVector3();

        npcCopy.transform.position = spawnVector;

        //Call event from EventManager
        em.EM_NPC_Spawned(npcCopy);
    }
    public void spawnNumOfNPC(int numToBeSpawned)
    {
        int spacesLeft = numOfSpacesLeft();
        if (spacesLeft < 1) { return; }

        if (spacesLeft >= numToBeSpawned)
        {
            for (int i = 0; i < numToBeSpawned; i++)
            {
                spawnNPC();
            }
        }
        else
        {
            spawnNumOfNPC(spacesLeft);
        }
    }
    public void spawnNumOfNPC(int numToBeSpawned, GameObject npc)
    {
        int spacesLeft = numOfSpacesLeft();
        if (spacesLeft < 1) { return; }

        if (spacesLeft >= numToBeSpawned)
        {
            for (int i = 0; i < numToBeSpawned; i++)
            {
                spawnNPC(npc);
            }
        }
        else
        {
            spawnNumOfNPC(spacesLeft, npc);
        }
    }
    public void spawnRandomNumOfNPC()
    {
        int spacesLeft = numOfSpacesLeft();
        if (spacesLeft < 1) { return; }
        
        int numToBeSpawned = Random.Range(numOfSpawnAtOnce, maxNumOfSpawnAtOnce);

        if (spacesLeft >= numToBeSpawned)
        {
            spawnNumOfNPC(numToBeSpawned);
        }
        else
        {
            spawnNumOfNPC(spacesLeft);
        }
    }
    public void spawnRandomNumOfNPC(GameObject npc)
    {
        int spacesLeft = numOfSpacesLeft();
        if (spacesLeft < 1) { return; }

        int numToBeSpawned = Random.Range(numOfSpawnAtOnce, maxNumOfSpawnAtOnce);

        if (spacesLeft >= numToBeSpawned)
        {
            spawnNumOfNPC(numToBeSpawned, npc);
        }
        else
        {
            spawnNumOfNPC(spacesLeft, npc);
        }
    }
    //@@
    public int pickRandomSpritePrefab()
    {
        int index = 0;
        int numOfPrefab = prefabNPC_List.Count;

        index = Random.Range(index, numOfPrefab);

        return index;
    }
    //@@
    //$$$$$$$$$$$$$$$$$$$$$$$
    //   $$$$$$$$$$$$$$$$
    //        $$$$$$



    //#########################
    //#### NPC COUNTING #######
    //##                    ###
    //#                       ####################################
    public void countAllNPC()
    {
        //Create array of all objects in scene with the tag NPC
        GameObject[] objects = GameObject.FindGameObjectsWithTag("NPC");

        if (objects == null)
        {
            Debug.Log("list of objects with tag is NULL");
            return;
        }

        numOfNPC = objects.Length;
    }
    public bool weHaveEnoughSpaceFor(int numOfNPCiWantToSpawn)
    {
        countAllNPC();

        if (numOfNPC + numOfNPCiWantToSpawn > maxNumOfNPC)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public int numOfSpacesLeft()
    {
        countAllNPC();

        int spacesLeft = maxNumOfNPC - numOfNPC;

        return spacesLeft;
    }
    //############################################################


    //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    //@@@@ VECTOR CALCULATIONS @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    //@@@
    //@@
    public Vector3 getSpawnVector3()
    {
        Vector3 generatedVector = new Vector2();

        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        generatedVector.x = x;
        generatedVector.y = y;
        generatedVector.z = 0;

        return generatedVector;
    }
    public Vector3 getSpawnVector3(float min_X, float max_X, float min_Y, float max_Y)
    {
        Vector3 generatedVector = new Vector2();

        float x = Random.Range(min_X, max_X);
        float y = Random.Range(min_Y, max_Y);

        generatedVector.x = x;
        generatedVector.y = y;
        generatedVector.z = 0;

        return generatedVector;
    }
    public Vector2 getSpawnVector2()
    {
        Vector2 generatedVector = new Vector2();

        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        generatedVector.x = x;
        generatedVector.y = y;

        return generatedVector;
    }
    public Vector2 getSpawnVector2(float min_X, float max_X, float min_Y, float max_Y)
    {
        Vector2 generatedVector = new Vector2();

        float x = Random.Range(min_X, max_X);
        float y = Random.Range(min_Y, max_Y);

        generatedVector.x = x;
        generatedVector.y = y;

        return generatedVector;
    }
    //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    
}
