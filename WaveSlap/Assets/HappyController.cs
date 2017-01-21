using UnityEngine;
using System.Collections;

public class HappyController : MonoBehaviour {

    public bool isPlayer;

    public int stepsToHappiness;

    public int currentStepToHappiness;

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
        if (Input.GetKeyDown(KeyCode.A)){
            IncreaseHappiness();
        }
    }

    void IncreaseHappiness() {
        if (currentStepToHappiness < stepsToHappiness) currentStepToHappiness++;

        if (happyCounter) happyCounter.color = new Color((float)currentStepToHappiness / stepsToHappiness, (float)currentStepToHappiness / stepsToHappiness, 0);
    }

    void OnTriggerEnter2D(Collider2D coll) {
        IncreaseHappiness();
    }
}
