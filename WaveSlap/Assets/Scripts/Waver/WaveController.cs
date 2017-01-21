﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveController : MonoBehaviour {

    public bool isPlayer;

    public int numWaves;

    Animator animator;

    public Vector2 chooseRandomWaveDelay;


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

    
    public float areaWaveRadius;
    public float singleWaveRange;
    public float singeWaveRadius;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

        if (!isPlayer) {
            StartCoroutine(TimedTriggerSingleWave());
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
        }
    }

	void TriggerAreaWave() {
        if (!animator.GetBool("Wave2")) {
            Collider2D[] collided = Physics2D.OverlapCircleAll(transform.position, areaWaveRadius);

            //force them out of their current animation
            animator.SetTrigger("Idle");
            //set them to do the area wave
            animator.SetTrigger("Wave2");

            foreach(Collider2D coll in collided) {
                WaveController waveController = coll.gameObject.GetComponent<WaveController>();
                if (waveController) {
                    waveController.TriggerAreaWave();
                }
            }
        }
    }

    IEnumerator TimedTriggerSingleWave() {
        while (gameObject.activeSelf) {
            yield return new WaitForSeconds(Random.Range(chooseRandomWaveDelay.x, chooseRandomWaveDelay.y));
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
                animator.SetTrigger("Wave1");
                SingleWave();
            }
        }

    }

    //Returns the transform of one object that is detected when you do a single wave
    Transform SingleWave() {
        LayerMask targetLayer = LayerMask.GetMask("Person");

        //Default right
        float xFacingDir = transform.localScale.x / Mathf.Abs(transform.localScale.x);

        Collider2D coll = Physics2D.OverlapCircle(transform.position + )
    }

    void HandleWaverInput() {

        switch (waveState)
        {
            case WaveState.Ready:

                if (Input.GetButtonDown("Wave1")) {
                    animator.SetTrigger("Wave1");
                }

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

    void SingleWaveback(Transform waved) {
        Transform waver = transform;

        Vector2 wavedToWaver = waver.position - waved.position;

        float xDir = wavedToWaver.x / Mathf.Abs(wavedToWaver.x);

        waved.localScale = new Vector3(-xDir * 0.5f, transform.localScale.y);

        WaveController waveController = GetComponent<WaveController>();

        if (waveController) {
            //Force out of their animation
            waveController.animator.SetTrigger("Idle");
            //Wave 
            waveController.animator.SetTrigger("Wave1");
        }
    }
}
