﻿using UnityEngine;
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

    public float darkestFloat;//default 0.333f
    public float colorScale;
    public float musicSpeedScale;//how fast the music play based on GlobalHappyScore
    public float animSpeedScale = 1f;//animation speed based on GlobalHappyScore

    public Color colorLense;
    public int colorScaleFunctionType;

    [SerializeField]
    private Transform m_happyBar;
    [SerializeField]
    private float m_minBarPosX;

    //Event
    public EventManager em;//MUST BE SET IN INSPECTOR

    void Awake()
    {
        NPCList = new List<GameObject>();//list of all NPC's game objects

        em = GameObject.Find("EventManager").GetComponent<EventManager>();

        //EVENTS
        em.HappinessIncreased += GHC_HappinessIncreased;
        em.HappinessDecreased += GHC_HappinessDecreased;
        em.NPCSpawned += GHC_NPCSpawned;
        em.WaverSpawned += GHC_WaverSpawned;
        em.InitialHordeSpawned += GHC_InitialHordeSpawned;
    }

    // Use this for initialization
    void Start () {
        colorLense = new Color(darkestFloat, darkestFloat, darkestFloat);

        if (m_happyBar) {
            m_minBarPosX = m_happyBar.GetComponent<RectTransform>().anchoredPosition.x;
        }
    }

    //@!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!@
    //@!!!!!!      EVENTS      !!!!!!@
    //@!!!!!!                  !!!!!!@
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
    private void GHC_NPCSpawned(object sender, NPCEventArgs e)
    {
        //Debug.Log("received message from GHC_NPCSpawned");

        //the NPC to be spawned is stored in the NPCEventArgs
        //as a gameObject variable
        addNPC(e.gameObject);

    }
    private void GHC_WaverSpawned(object sender, NPCEventArgs e)
    {
        //Debug.Log("received message from GHC_WaverSpawned");

        addNPC_byTag("Player", true);
        shuffleList();
    }
    private void GHC_InitialHordeSpawned(object sender, NPCEventArgs e)
    {
        //Debug.Log("received message from GHC_InitialHordeSpawned");

        findAllNPC();
        calcGlobalHappyScore();
        applyColorScaleType();
    }
    //@!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!@

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
            //Add all NPC tagged game objects to our NPCList
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
    public void addNPC_byTag(string tag, bool reCalcStats)
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag(tag);
        NPCList.Add(gameObject);

        if (reCalcStats)
        {
            calcGlobalHappyScore();
        }
    }

    //~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~
    //~*~*~        COLOR CHANGING FUNCTIONS        *~*~*~
    //~*~*~
    public void applyColorScaleType()
    {
        switch (colorScaleFunctionType)
        {
            case 0:
                //apply default colorScale
                //gradually increases brightness
                applyColorScale();
                break;
            case 1:
                //apply a gradual darkening to a selected color
                //based on the position of the game object in the list of game objects
                //3 different colors
                applyColorScale_Type2();
                break;
            case 2:
                //apply a gradual darkening to a selected color
                //based on the position of the game object in the list of game objects
                //6 different colors
                applyColorScale_Type3();
                break;
            case 3:
                //apply random color scale of one color RGB
                //as well as an increase in darkness to that color
                //randomly changes each sprite's color every time
                applyRandomColorScaleRandomly();
                break;
        }
    }
    public void applyColorScale_Type2()
    {
        int mod3 = 0;
        foreach (GameObject curGameObject in NPCList)
        {
            spriteRenderer = curGameObject.GetComponent<SpriteRenderer>();
            mod3 = (NPCList.IndexOf(curGameObject)) % 3;

            switch (mod3)
            {
                case 0:
                    spriteRenderer.color = new Color(1f, (1f - colorLense.g), (1f - colorLense.b));
                    break;
                case 1:
                    spriteRenderer.color = new Color((1f - colorLense.r), 1f, (1f - colorLense.b));
                    break;
                case 2:
                    spriteRenderer.color = new Color((1f - colorLense.r), (1f - colorLense.g), 1f);
                    break;
            }
        }
    }
    public void applyColorScale_Type3()
    {
        int mod6 = 0;
        foreach (GameObject curGameObject in NPCList)
        {
            spriteRenderer = curGameObject.GetComponent<SpriteRenderer>();
            mod6 = (NPCList.IndexOf(curGameObject)) % 6;

            switch (mod6)
            {
                case 0:
                    spriteRenderer.color = new Color(1f, (1f - colorLense.g), (1f - colorLense.b));
                    break;
                case 1:
                    spriteRenderer.color = new Color((1f - colorLense.r), 1f, (1f - colorLense.b));
                    break;
                case 2:
                    spriteRenderer.color = new Color((1f - colorLense.r), (1f - colorLense.g), 1f);
                    break;
                case 3:
                    spriteRenderer.color = new Color(1f, 1f, (1f - colorLense.b));
                    break;
                case 4:
                    spriteRenderer.color = new Color(1f, (1f - colorLense.g), 1f);
                    break;
                case 5:
                    spriteRenderer.color = new Color((1f - colorLense.r), 1f, 1f);
                    break;

            }
        }
    }
    public void applyRandomColorScaleRandomly()
    {
        foreach (GameObject curGameObject in NPCList)
        {
            spriteRenderer = curGameObject.GetComponent<SpriteRenderer>();
            int randSelect = Random.Range(1, 4);
            switch (randSelect)
            {
                case 1:
                        spriteRenderer.color = new Color(1f, (1f - colorLense.g),(1f - colorLense.b));
                    break;
                case 2:
                        spriteRenderer.color = new Color((1f - colorLense.r), 1f, (1f - colorLense.b));
                    break;
                case 3:
                        spriteRenderer.color = new Color((1f - colorLense.r), (1f - colorLense.g), 1f);
                    break;
            }
        }
    }
    public void applyColorScale()
    {
        foreach (GameObject curGameObject in NPCList)
        {
            spriteRenderer = curGameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.color = colorLense;
        }
    }
    //~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~

    //@@@@@@@@@@@@@@@@@@@@@@@@
    //@@ ANIM + MUSIC SPEED @@
    public void applyAnimSpeedScale()
    {

    }
    public void applyMusicSpeedScale()
    {

    }

    //@@@@@@@@@@@@@@@@@@
    //@@ SHUFFLE CODE @@
    public void shuffleList()
    {
        int listSize = NPCList.Count;

        for (int i = 0; i < listSize; i++)
        {
            GameObject temp = NPCList[i];
            int randomIndex = Random.Range(i, listSize);
            NPCList[i] = NPCList[randomIndex];
            NPCList[randomIndex] = temp;
        }
    }
    public void shuffleList<T>(List<T> ts)
    {
        //public static void Shuffle<T>(this IList<T> ts)
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }


    //@@@@@@@@@@@@@@@@@@@@@@@@
    //@@@@@ CALCULATIONS @@@@@
    //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    public void calcGlobalHappyScore()
    {
        //Iterate through all game objects in our NPCList
        //sum all of their HappyController component's currentHappiness amounts
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

        if (m_happyBar) {
            // "fixed" scores to remove the starting at 1 happiness...
            float fixedTotalHappyScore = totalHappyScore - numOfNPC;
            float maxHappinessLevel = maxHappinessLevels - 1;


            //Assumes 0 = full bar
            float range = Mathf.Abs(m_minBarPosX);
            float percHappy = fixedTotalHappyScore / (maxHappinessLevel * numOfNPC);
            float posFromStart = range * percHappy;


            float pos = m_minBarPosX + posFromStart;
            Debug.Log(pos);

            Vector3 vecPos = m_happyBar.GetComponent<RectTransform>().anchoredPosition;
            vecPos.x = pos;

            m_happyBar.GetComponent<RectTransform>().anchoredPosition = vecPos;
        }

        if(totalHappyScore >= maxHappinessLevels * numOfNPC) {
            Debug.Log("Waver Wins");
        }

        //update the 
        //musicSpeedScale
        updateMusicSpeed();
        //colorScale
        updateColorScale();
        //animSpeedScale
        updateanimSpeedScale();

        //apply scales
        applyColorScaleType();
        applyAnimSpeedScale();
        applyMusicSpeedScale();
    }
    void updateanimSpeedScale()
    {
        animSpeedScale = GlobalHappyScore * animSpeedCoeffecient;
    }
    void updateMusicSpeed()
    {
        musicSpeedScale = GlobalHappyScore * musicSpeedCoeffecient;
    }
    void updateColorScale()
    {
        //HAS TO BE BETWEEN 0 AND 1
        colorScale = GlobalHappyScore;

        colorScale += darkestFloat;

        if (colorScale > 1f)
        {
            colorScale = 1f;
        }

        colorLense = new Color(colorScale, colorScale, colorScale);

        //Debug.Log("ColorScale = " + colorScale);
    }
    //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

}
