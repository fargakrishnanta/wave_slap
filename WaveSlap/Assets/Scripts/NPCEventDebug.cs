using UnityEngine;
using System.Collections;

public class NPCEventDebug : MonoBehaviour {


    public event NPCEventHandler Event1;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.E))
        {
            NPCEventDebugCall();
        }
    }

    public void NPCEventDebugCall()
    {
        if (Event1 != null)
        {
            Event1(this, new NPCEventArgs());
        }
    }
    /* 
    in another class that has an instance of this object....

    in start...

    npcDebug = GetComponent<NPCEventDebug>();

    npcDebug.Event1 += Event1_Debug;

    and then you have the method

    private void Event1_Debug(object sender, NPCEventArgs e)
    {
        Debug.Log("received message from Event1_Debug");
    }
    
    
    */
}
