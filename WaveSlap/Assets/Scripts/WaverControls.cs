using UnityEngine;
using System.Collections;

public class WaverControls : MonoBehaviour {


    public Rigidbody2D rigidBody;

    

    //Movement Variables
    public bool moveKeysPressed;

    public float moveSpeed = 2f;

    public Vector3 moveVector = new Vector3(0, 0, 0);
    public Vector3 moveVectorOld = new Vector3(1, 0, 0);
    public Vector3 sumVector = new Vector3(0, 0, 0);

    public Vector3 rigidBodyVector;
    public int rigidBodyVelocityX;
    public int rigidBodyVelocityY;

    // Use this for initialization
    void Start () {

        moveVector = new Vector3(0, 0, 0);
        moveVectorOld = new Vector3(1, 0, 0);
        sumVector = new Vector3(0, 0, 0);

        rigidBodyVector = new Vector3(0, 0, 0);
        rigidBodyVelocityX = 0;
        rigidBodyVelocityY = 0;
    }
	
	// Update is called once per frame
	void Update () {

        //Check to see if we pressed movement keys and moves player accordingly
        checkMovementKeys();

        //Check to see if we pressed any of the action keys and apply action functions
        checkActionKeys();

    }

    void checkMovementKeys()
    {
        float x = 0;
        float y = 0;

        moveKeysPressed = false;

        //check to see if the user has pressed any of the movement keys
        if (Input.GetKey(KeyCode.W))
        {
            y = 1;
            moveKeysPressed = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            x = -1;
            moveKeysPressed = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            y = -1;
            moveKeysPressed = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            x = 1;
            moveKeysPressed = true;
        }

        moveVector.x = x;
        moveVector.y = y;
        moveVector.Normalize();

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

    public void checkActionKeys()
    {
        //Check to see if the user has pressed the Wave key
        //Call player wave function
        if (Input.GetKeyDown(KeyCode.Space))
        {
            waveInDirection(moveVectorOld);
        }
    }

    public void movePlayer(Vector3 v)
    {
        //Obtain rigidBody2D component
        rigidBody = this.GetComponent<Rigidbody2D>();

        //Calculate and apply new veloctiy 
        //by multiplying player speed by the given direction vector
        rigidBody.velocity.Set(v.x * moveSpeed, v.y * moveSpeed);
    }

    public void waveInDirection(Vector3 v)
    {

    }
}
