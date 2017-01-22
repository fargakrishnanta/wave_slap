using UnityEngine;
using System.Collections;

public class InstructionSlapperCutSceneScript : MonoBehaviour {


    [SerializeField]
    public float MaxTimer;

    public GameManager gm;

    // Update is called once per frame
    void Update()
    {
        MaxTimer -= Time.fixedDeltaTime;
        if (MaxTimer <= 0)
            gm.InGame();
    }
}
