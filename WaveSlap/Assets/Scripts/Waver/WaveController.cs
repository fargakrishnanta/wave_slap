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

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

        if (!isPlayer) {
            StartCoroutine(ChooseRandomWave());
        }
	}

    // Update is called once per frame
    void Update() {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Idle")) {
            if (isPlayer) {
                HandleWaverInput();
                GetComponent<WaverControls>().enabled = true;
            }


        }
        else {
            if (isPlayer) {
                GetComponent<WaverControls>().enabled = false;
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


            //If it's the player, increase the happiness of all in area
            if (isPlayer) MakeOthersHappy(collided);

            foreach(Collider2D coll in collided) {
                WaveController waveController = coll.gameObject.GetComponent<WaveController>();
                if (waveController) {
                    waveController.TriggerAreaWave();
                }
            }
        }
    }

    void MakeOthersHappy(Collider2D[] targets) {
        foreach(Collider2D target in targets) {
            HappyController happyController = target.GetComponent<HappyController>();
            

            if (happyController) {
                happyController.IncreaseHappiness();
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
        if (Input.GetButtonDown("Wave2")) {
            TriggerAreaWave();
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
}
