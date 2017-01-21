using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {

    public bool isPlayer;

    public int numWaves;

    Animator animator;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlayer && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
            HandleWaveInput();
        }
	}

    void HandleWaveInput() {
        for(int i=1; i<=numWaves; i++) {
            if (Input.GetButtonDown("Wave" + i)) {
                animator.SetTrigger("Wave" + i);
            }
        }
    }
}
