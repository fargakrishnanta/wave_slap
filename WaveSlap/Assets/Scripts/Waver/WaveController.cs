using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveController : MonoBehaviour {

    public bool isPlayer;

    public int numWaves;

    Animator animator;

    public Vector2 chooseRandomWaveDelay;

    public List<GameObject> waveBoxes;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

        if (!isPlayer) {
            StartCoroutine(ChooseRandomWave());
        }
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
        for(int i=1; i<=numWaves; i++) {
            if (Input.GetButtonDown("Wave" + i)) {
                animator.SetTrigger("Wave" + i);
            }
        }
    }

    void ChooseRandom() {

    }
}
