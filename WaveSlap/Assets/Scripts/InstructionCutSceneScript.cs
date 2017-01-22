using UnityEngine;
using System.Collections;

public class InstructionCutSceneScript : MonoBehaviour {

    [SerializeField]
    public float MaxTimer;

    public GameManager gm;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        MaxTimer -= Time.fixedDeltaTime;
        if (MaxTimer <= 0)
            gm.StartSlapperCutScene();
	}
}
