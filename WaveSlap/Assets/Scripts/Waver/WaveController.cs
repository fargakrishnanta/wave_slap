using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveController : MonoBehaviour {

    public bool isPlayer;

    public int numWaves;

    Animator animator;

    public Vector2 chooseRandomWaveDelay;

    public float areaWaveRadius;

    public int currWave;

	/// <summary>
    /// Use this state info to reduce amount of calls
    /// </summary>
    private AnimatorStateInfo stateInfo;

    public WaveState waveState = WaveState.Ready;

    //Wave cool down and state variables
    public enum WaveState
    {
        Ready,
        Waving,
        CoolDown
    }
    public float coolDownDuration;
    public float coolDownTimer;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

        if (!isPlayer) {
            StartCoroutine(ChooseRandomWave());
        }

        //Initialize the coolDownTimer to be the coolDownDuration
        coolDownTimer = coolDownDuration;

        waveState = WaveState.Ready;
	}

    // Update is called once per frame
    void Update() {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Idle")) {
            if (isPlayer) {
                HandleWaverInput();
                GetComponent<WaverMovement>().enabled = true;
            }


        }
        else {
            if (isPlayer) {
                GetComponent<WaverMovement>().enabled = false;
            }
            else {
                GetComponent<NPCMovementControl>().Stop();
            }

            //Handle animations
            HandleSingleWave();
        }
    }

	void HandleSingleWave() {
        if (!stateInfo.IsName("SingleWave")) return;

		//TO DO
    }

	void TriggerAreaWave() {
        if (!animator.GetBool("Wave2")) {
            Collider2D[] collided = Physics2D.OverlapCircleAll(transform.position, areaWaveRadius);

            animator.SetTrigger("Idle");
            animator.SetTrigger("Wave2");

            if (isPlayer) MakeOthershappy(collided);

            foreach(Collider2D coll in collided) {
                WaveController waveController = coll.gameObject.GetComponent<WaveController>();
                if (waveController) {
                    waveController.TriggerAreaWave();
                }
            }
        }
    }

    IEnumerator ChooseRandomWave() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(chooseRandomWaveDelay.x, chooseRandomWaveDelay.y));
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
                currWave = Random.Range(1, numWaves + 1);
                animator.SetTrigger("Wave" + currWave);
            }
        }

    }

    void HandleWaverInput() {

        switch (waveState)
        {
            case WaveState.Ready:
                if (Input.GetButtonDown("Wave2"))
                {
                    TriggerAreaWave();
                    waveState = WaveState.Waving;
                }

                break;
            case WaveState.Waving:

                //removed the timer for waiting for the waving animation
                //to finish because i think that time is so small
                //its not worth it to count
                //its prob like 1 second might as well add it to the
                //cool down timer initially

                waveState = WaveState.CoolDown;

                break;
            //Bro, stop
            case WaveState.CoolDown:
                //coolDownTimer -= Time.fixedDeltaTime;
                coolDownTimer -= Time.fixedDeltaTime;

                if (coolDownTimer <= 0)
                {
                    coolDownTimer = coolDownDuration;
                    waveState = WaveState.Ready;
                }
                break;
        }
        
    }

	//Use for single later
    void TurnWavedToSelf(CollisionTrigger2D trigger, Collider2D coll) {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Person")) {
            Transform waved = coll.transform;
            Transform waver = transform;

            Vector2 wavedToWaver = waver.position - waved.position;

            float xDir = wavedToWaver.x / Mathf.Abs(wavedToWaver.x);

            waved.localScale = new Vector3(-xDir * 0.5f, transform.localScale.y);
        }
    }

    void MakeOthershappy(Collider2D[] targets)
    {
        foreach(Collider2D target in targets)
        {
            HappyController happyController = target.GetComponent<HappyController>();
            if (happyController)
            {
                happyController.IncreaseHappiness();
            }
        }
    }
}
