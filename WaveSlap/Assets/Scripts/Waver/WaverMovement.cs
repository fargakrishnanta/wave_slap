using UnityEngine;
using System.Collections;

public class WaverMovement : MonoBehaviour {


    public Rigidbody2D rigidBody;

    public bool isSlapped = false; //When true, waver player has lost

    //Movement Variables
    public bool moveKeysPressed;

    public float moveSpeed = 2f;

    public Vector3 moveVector = new Vector3(0, 0, 0);
    public Vector3 moveVectorOld = new Vector3(1, 0, 0);

    public Vector3 rigidBodyVector;
    public int rigidBodyVelocityX;
    public int rigidBodyVelocityY;

    public bool printMovementDebug = false;

    // Use this for initialization
    void Start () {

        moveVector = new Vector3(0, 0, 0);
        moveVectorOld = new Vector3(1, 0, 0);

        rigidBodyVector = new Vector3(0, 0, 0);
        rigidBodyVelocityX = 0;
        rigidBodyVelocityY = 0;
    }
	
	// Update is called once per frame
	void Update () {

        //Check to see if we pressed movement keys and moves player accordingly
        checkMovementKeys();

    }

    public void Stop()
    {
        rigidBody.velocity = new Vector2(0f, 0f);
    }

    void checkMovementKeys()
    {

        float x = 0;
        float y = 0;

        moveKeysPressed = false;

        y = Input.GetAxis("Vertical");
        x = Input.GetAxis("Horizontal");

        //check to see if the user has pressed any of the movement keys
        if (x != 0 || y != 0)
        {
            moveKeysPressed = true;

            //x and y are numbers between 0 and 1, or 0 and -1
            //they can be float values so we need to convert them to
            //either 1 or -1 respectively
            if (x > 0)
            {
                x = 1;
            }
            else if (x < 0)
            {
                x = -1;
            }

            if (y > 0)
            {
                y = 1;
            }
            else if (y < 0)
            {
                y = -1;
            }
        }

        moveVector.x = x;
        moveVector.y = y;
        //moveVector.Normalize();

        //Check to see if the user has pressed any of the movement keys
        //Call player movement function
        if (moveKeysPressed)
        {
            movePlayer(moveVector);
        }
        

        //Set facing direction
        if (x != 0 || y != 0)
        {
            moveVectorOld = moveVector;
        }
    }

    public void movePlayer(Vector3 v)
    {
        if (printMovementDebug)
        {
           Debug.Log("movePlayer[x][y]: [" + v.x + "][" + v.y + "]");
        }

        //Obtain rigidBody2D component
        rigidBody = this.GetComponent<Rigidbody2D>();

        //Calculate and apply new velocity 
        //by multiplying player speed by the given direction vector
        //rigidBody.velocity.Set(v.x * moveSpeed, v.y * moveSpeed);

        Vector2 currentPos = rigidBody.position;
        Vector2 newPos = new Vector2();
        newPos.x = currentPos.x + (v.x * (moveSpeed * Time.deltaTime));
        newPos.y = currentPos.y + (v.y * (moveSpeed * Time.deltaTime));
        //rigidBody.MovePosition(new Vector2(v.x * (moveSpeed * Time.deltaTime), v.y * (moveSpeed * Time.deltaTime)));

        rigidBody.MovePosition(newPos);

        //Below method never stops moving unless you check to see if we are still trying to move
        //rigidBody.velocity = new Vector2(v.x * moveSpeed, v.y * moveSpeed);

        //Flip Sprite
        transform.localScale = 
            new Vector3(moveVectorOld.x == 0 ? transform.localScale.x : -moveVectorOld.x * 1f, transform.localScale.y);
    }

}
