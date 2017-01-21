using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {

    public bool isPlayer;

    public int numWaves;

    Animator animator;

    public Vector2 chooseRandomWaveDelay;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

        if (!isPlayer) {
            StartCoroutine(ChooseRandomWave());
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlayer && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
            HandleWaveInput();
        }
	}

    IEnumerator ChooseRandomWave() {

        while (true) {
            yield return new WaitForSeconds(Random.Range(chooseRandomWaveDelay.x, chooseRandomWaveDelay.y));
            Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"));
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
                Debug.Log("BB");
                animator.SetTrigger("Wave" + Random.Range(1, numWaves));
            }
        }

    }

    void HandleWaveInput() {
        for(int i=1; i<=numWaves; i++) {
            if (Input.GetButtonDown("Wave" + i)) {
                animator.SetTrigger("Wave" + i);
            }
        }
    }

    void ChooseRandom() {

    }
}
