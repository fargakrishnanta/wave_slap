using UnityEngine;
using System.Collections;

public class MovementControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void FlipIt(Vector2 dir) {
        if (dir.x > 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else if (dir.x < 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }
}
