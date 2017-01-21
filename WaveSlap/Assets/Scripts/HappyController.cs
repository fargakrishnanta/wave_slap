using UnityEngine;
using System.Collections;

public class HappyController : MonoBehaviour {

    public bool isPlayer;

    public int maxHappiness;//Max Happiness
    public int minHappiness = 0;//Min Happiness, assuming the NPC is not enrolled at BCIT

    public int currentHappiness;//current Happiness

    public SpriteRenderer happyCounter;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (!isPlayer) HandleNPCHappiness();
	}

    void HandleNPCHappiness() {
        //temp trigger
        if (Input.GetKeyDown(KeyCode.H)){
            IncreaseHappiness();
        }
    }

    void IncreaseHappiness() {
        if (currentHappiness < maxHappiness) currentHappiness++;

        if (happyCounter) happyCounter.color = new Color((float)currentHappiness / maxHappiness, (float)currentHappiness / maxHappiness, 0);
    }
    void DecreaseHappiness()
    {
        if (currentHappiness < minHappiness) currentHappiness--;

        if (happyCounter) happyCounter.color = new Color((float)currentHappiness / maxHappiness, (float)currentHappiness / maxHappiness, 0);
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if(coll.GetComponent<HappyController>()) coll.GetComponent<HappyController>().IncreaseHappiness();
    }
}
