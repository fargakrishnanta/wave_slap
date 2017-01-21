using UnityEngine;
using System.Collections;

public class WaverMovement : MonoBehaviour {


    public Rigidbody2D rigidBody;

    public bool isSlapped = false; //When true, waver player has lost

    public float moveSpeed = 2f;


    // Use this for initialization
    void Start () {

        rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
      
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        var moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigidBody.MovePosition(rigidBody.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        FlipIt(moveDirection);
    }

    void FlipIt(Vector2 dir)
    {
        if (dir.x > 0)
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else if(dir.x < 0)
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

}
