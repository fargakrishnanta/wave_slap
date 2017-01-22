using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HappyController : MonoBehaviour {

    public bool isPlayer;

    public int maxHappiness;//Max Happiness
    public int minHappiness = 0;//Min Happiness, assuming the NPC is not enrolled at BCIT

    public int currentHappiness;//current Happiness

    public SpriteRenderer happyCounter;

    //Event Manager
    public EventManager em;

    public float delayDeath;

    [Header("Happy Level Animators")]
    public List<AnimatorOverrideController> happyLevels;

    private Animator animator;

    // Use this for initialization
    void Start () {
        em = GameObject.Find("EventManager").GetComponent<EventManager>();
        animator = GetComponent<Animator>();

        animator.runtimeAnimatorController = happyLevels[0];
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
        if (Input.GetKeyDown(KeyCode.J))
        {
            DecreaseHappiness();
        }



    }

    


    public void IncreaseHappiness() {
        if (currentHappiness < maxHappiness) currentHappiness++;

        if (happyCounter) happyCounter.color = new Color((float)currentHappiness / maxHappiness, (float)currentHappiness / maxHappiness, 0);

        //-1 because we start at 1...
        animator.runtimeAnimatorController = happyLevels[currentHappiness - 1];

        //Call the EM_HappinessIncreased function in the Event Manager
        //This in turn lets the GlobalHappinessController know 
        //that someone's happiness has increased, prompting it to
        //recalculate the total happiness

        if (!isPlayer && currentHappiness >= maxHappiness) {
           // StartCoroutine(Death(delayDeath));
            
        }

        em.EM_HappinessIncreased();
    }

    IEnumerator Death(float delay) {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    public void IncreaseHappiness(int delta) {
        currentHappiness = Mathf.Min(currentHappiness + delta, maxHappiness);

        if (happyCounter) happyCounter.color = new Color((float)currentHappiness / maxHappiness, (float)currentHappiness / maxHappiness, 0);

        //Call the EM_HappinessIncreased function in the Event Manager
        //This in turn lets the GlobalHappinessController know 
        //that someone's happiness has increased, prompting it to
        //recalculate the total happiness
        em.EM_HappinessIncreased();
    }

    void DecreaseHappiness()
    {
        if (currentHappiness > minHappiness) currentHappiness--;

        if (happyCounter) happyCounter.color = new Color((float)currentHappiness / maxHappiness, (float)currentHappiness / maxHappiness, 0);

        //Call the EM_HappinessDecreased function in the Event Manager
        //This in turn lets the GlobalHappinessController know 
        //that someone's happiness has decreased, prompting it to
        //recalculate the total happiness
        em.EM_HappinessDecreased();
    }

    void DecreaseHappiness(int delta) {
        currentHappiness = Mathf.Max(currentHappiness + delta, minHappiness);

        if (happyCounter) happyCounter.color = new Color((float)currentHappiness / maxHappiness, (float)currentHappiness / maxHappiness, 0);

        //Call the EM_HappinessDecreased function in the Event Manager
        //This in turn lets the GlobalHappinessController know 
        //that someone's happiness has decreased, prompting it to
        //recalculate the total happiness
        em.EM_HappinessDecreased();
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if(coll.GetComponent<HappyController>()) coll.GetComponent<HappyController>().IncreaseHappiness();
    }
}
