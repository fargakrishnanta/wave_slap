using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalHappyController : MonoBehaviour {

    public int numOfNPC;
    public List<HappyController> NPCList;
    public HappyController currentHappyController;

    public float GlobalHappyScore;
    public float maxHappinessPerNPC = 3f;

    public float colorScale;
    public float musicSpeedScale;//how fast the music play based on GlobalHappyScore
    public float animSpeed = 1f;//animation speed based on GlobalHappyScore

    // Use this for initialization
    void Start () {
        NPCList = new List<HappyController>();
        currentHappyController = new HappyController();


    }

    public void findAllNPC()
    {
        //Create array of all objects in scene with the tag NPC
        GameObject[] objects = GameObject.FindGameObjectsWithTag("NPC");

        if (objects == null)
        {
            Debug.Log("list of objects with tag is NULL");
            return;
        }

        int objectCount = objects.Length;

        if (objectCount == 0)
        {
            Debug.Log("objectCount is 0");
        }

        //iterate through all those NPC objects
        foreach (GameObject obj in objects)
        {
            //Obtain each NPC's HappyController
            currentHappyController = obj.GetComponent<HappyController>();
            //Add that NPC's HappyController to the list of all HappyControllers
            NPCList.Add(currentHappyController);
        }

        //Update numOfNPC's
        numOfNPC = NPCList.Count;
    }

    public void calcGlobalHappyScore()
    {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addNPC(HappyController NPC)
    {
        NPCList.Add(NPC);
        numOfNPC = NPCList.Count;//Update numOfNPC's
    }
    void updateAnimSpeed()
    {
        animSpeed = GlobalHappyScore / ((float)numOfNPC / maxHappinessPerNPC);
    }
    void updateMusicSpeed()
    {
        musicSpeedScale = GlobalHappyScore / ((float)numOfNPC / maxHappinessPerNPC);
    }

}
