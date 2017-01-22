using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalHappyController : MonoBehaviour {

    public float maxHappinessLevels = 3.0f;

    public float animSpeedCoeffecient = 1.8f;
    public float musicSpeedCoeffecient = 1.8f;

    public int numOfNPC;
    public List<GameObject> NPCList;

    SpriteRenderer spriteRenderer;

    public float GlobalHappyScore;
    public float maxHappinessPerNPC = 3f;

    public float colorScale;
    public float musicSpeedScale;//how fast the music play based on GlobalHappyScore
    public float animSpeedScale = 1f;//animation speed based on GlobalHappyScore

    public Color colorLense = new Color(0.3f, 0.3f, 0.3f);

    //Event
    public EventManager em;//MUST BE SET IN INSPECTOR

    // Use this for initialization
    void Start () {
        NPCList = new List<GameObject>();//list of all NPC's game objects

        colorLense = new Color(1f, 1f, 1f);

        //EVENTS
        em.HappinessIncreased += GHC_HappinessIncreased;
        em.HappinessDecreased += GHC_HappinessDecreased;

        findAllNPC();
    }
    private void GHC_HappinessIncreased(object sender, NPCEventArgs e)
    {
        //Debug.Log("received message from GHC_HappinessIncreased");
        calcGlobalHappyScore();
    }
    private void GHC_HappinessDecreased(object sender, NPCEventArgs e)
    {
        //Debug.Log("received message from GHC_HappinessDecreased");
        calcGlobalHappyScore();
    }

    // Update is called once per frame
    void Update () {
	
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
            //currentHappyController = obj.GetComponent<HappyController>();

            //Add that NPC's HappyController to the list of all HappyControllers
            //NPCList.Add(currentHappyController);
            NPCList.Add(obj);
        }

        //Update numOfNPC's
        numOfNPC = NPCList.Count;
    }
    public void addNPC(GameObject NPC)
    {
        NPCList.Add(NPC);
        numOfNPC = NPCList.Count;//Update numOfNPC's

        calcGlobalHappyScore();
    }

    public void applyColorScale()
    {
        foreach (GameObject curGameObject in NPCList)
        {
            spriteRenderer = curGameObject.GetComponent<SpriteRenderer>();
            //spriteRenderer.color = new Color(1f, 0, 0);
            spriteRenderer.color = colorLense;
        }
        //Debug.Log("ColorScale = " + colorScale);
    }
    public void applyAnimSpeedScale()
    {

    }
    public void applyMusicSpeedScale()
    {

    }

    //@@@@@@@@@@@@@@@@@@@@@@@@
    //@@@@@ CALCULATIONS @@@@@
    //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    public void calcGlobalHappyScore()
    {
        int totalHappyScore = 0;
        foreach (GameObject curGameObject in NPCList)
        {
            totalHappyScore += curGameObject.GetComponent<HappyController>().currentHappiness;
        }

        numOfNPC = NPCList.Count;

        //Global Happy Score is represented from 0 to 1
        //where 0 is everyone is at 0 happiness (not possible)
        //lowest possible is 0.333
        //and 1 is everyone is at 3 happiness
        GlobalHappyScore = ((float)totalHappyScore) / maxHappinessLevels;
        GlobalHappyScore = GlobalHappyScore / ((float)numOfNPC);

        //update the 
        //musicSpeedScale
        updateMusicSpeed();
        //colorScale
        updateColorScale();
        //animSpeedScale
        updateanimSpeedScale();

        //apply scales
        applyColorScale();
        applyAnimSpeedScale();
        applyMusicSpeedScale();
    }
    void updateanimSpeedScale()
    {
        animSpeedScale = GlobalHappyScore * 1.8f;
    }
    void updateMusicSpeed()
    {
        musicSpeedScale = GlobalHappyScore * 1.8f;
    }
    void updateColorScale()
    {
        //HAS TO BE BETWEEN 0 AND 1
        colorScale = GlobalHappyScore;

        colorLense = new Color(colorScale, colorScale, colorScale);
    }
    //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

}
