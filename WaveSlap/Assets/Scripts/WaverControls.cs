using UnityEngine;
using System.Collections;

public class WaverControls : MonoBehaviour {


    public Rigidbody2D rigidBody;

    public bool isSlapped = false; //When true, waver player has lost

    //Wave Flag Variable
    public enum waveTypeFlag { Wave1, Wave2, Wave3 };

    public bool isWaving;
    public bool isWavingType1;
    public bool isWavingType2;
    public bool isWavingType3;

    //Movement Variables
    public bool moveKeysPressed;

    public float moveSpeed = 2f;

    public Vector3 moveVector = new Vector3(0, 0, 0);
    public Vector3 moveVectorOld = new Vector3(1, 0, 0);
    public Vector3 sumVector = new Vector3(0, 0, 0);

    public Vector3 rigidBodyVector;
    public int rigidBodyVelocityX;
    public int rigidBodyVelocityY;

    //@@@
    //DEBUG FLAGS
    //@@@
    public bool printMovementDebug = true;
    public bool printWaveActionDebug = true;
    //@@@@

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

        y = Input.GetAxis("Vertical");
        x = Input.GetAxis("Horizontal");

        //check to see if the user has pressed any of the movement keys
        if (x != 0 || y != 0)
        {
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
        isWavingType1 = false;
        isWavingType2 = false;
        isWavingType3 = false;

        //Check to see if the user has pressed the Wave key
        //Call player wave function
        if (Input.GetButton("Wave1"))
        {
            waveInDirection(moveVectorOld, (int)waveTypeFlag.Wave1);
            isWavingType1 = true;
        }
        else if (Input.GetButton("Wave2"))
        {
            waveInDirection(moveVectorOld, (int)waveTypeFlag.Wave2);
            isWavingType2 = true;
        }
        else if (Input.GetButton("Wave3"))
        {
            waveInDirection(moveVectorOld, (int)waveTypeFlag.Wave3);
            isWavingType3 = true;
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

        //Calculate and apply new veloctiy 
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


    }

    public void waveInDirection(Vector3 v, int waveTypeFlag)
    {
        switch (waveTypeFlag)
        {
            case 0://Wave Type 1
                //
                waveInDirection_Type1(v);
                break;
            case 1://Wave Type 2
                //
                waveInDirection_Type2(v);
                break;
            case 2://Wave Type 3
                //
                waveInDirection_Type3(v);
                break;
        }
    }
    public void waveInDirection_Type1(Vector3 v)
    {
        if (printWaveActionDebug)  Debug.Log("waveInDirection_Type1 hit");
    }
    public void waveInDirection_Type2(Vector3 v)
    {
        if (printWaveActionDebug) Debug.Log("waveInDirection_Type2 hit");
    }
    public void waveInDirection_Type3(Vector3 v)
    {
        if (printWaveActionDebug) Debug.Log("waveInDirection_Type3 hit");
    }
}
