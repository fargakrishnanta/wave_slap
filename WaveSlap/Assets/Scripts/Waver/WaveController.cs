using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveController : MonoBehaviour {

    public bool isPlayer;

    public int numWaves;

    Animator animator;

    public Vector2 chooseRandomWaveDelay;

    public List<GameObject> waveBoxes;

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
	void Update () {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
            DeactivateAllWaveBoxes();
            if(isPlayer) HandleWaverInput();
        }
        else {
            for(int i = 1; i<numWaves; i++) {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Wave" + i)) {
                    ActivateWaveBox(i-1);
                }
            }
        }
	}

    void DeactivateAllWaveBoxes() {
        foreach(GameObject obj in waveBoxes) {
            obj.SetActive(false);
        }
    }

    void ActivateWaveBox(int ndex) {
        waveBoxes[ndex].SetActive(true);
    }

    IEnumerator ChooseRandomWave() {

        while (true) {
            yield return new WaitForSeconds(Random.Range(chooseRandomWaveDelay.x, chooseRandomWaveDelay.y));
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
                animator.SetTrigger("Wave" + Random.Range(1, numWaves));
            }
        }

    }

    void HandleWaverInput() {

        switch (waveState)
        {
            case WaveState.Ready:

                for (int i = 1; i <= numWaves; i++)
                {

                    if (Input.GetButtonDown("Wave" + i))
                    {
                        Debug.Log("waveInDirection_Type " + i + " hit");
                        animator.SetTrigger("Wave" + i);
                        waveState = WaveState.Waving;

                        //Run Waver Controls
                    }
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

    void ChooseRandom() {

    }
}
