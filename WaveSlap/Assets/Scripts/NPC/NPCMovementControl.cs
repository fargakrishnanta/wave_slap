using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(Rigidbody2D))]
public class NPCMovementControl : MonoBehaviour {



    Transform wanderTarget;
    Rigidbody2D rb;
    List<Vector2> availableDirections;

    public Vector2 changeDirectionDelayRange;
    public float movementSpeed;

    public Vector2 currentDirection;
    public bool isStop = false;
    

    // Use this for initialization
    void Start() {
        wanderTarget = transform.FindChild("Wander Target");
        rb = GetComponent<Rigidbody2D>();
        availableDirections = new List<Vector2>();

        //Refactor
        availableDirections.Add(Vector2.up);
        availableDirections.Add(Vector2.down);
        availableDirections.Add(Vector2.left);
        availableDirections.Add(Vector2.right);
        availableDirections.Add(new Vector2(1, 1));
        availableDirections.Add(new Vector2(-1, 1));
        availableDirections.Add(new Vector2(1, -1));
        availableDirections.Add(new Vector2(-1, -1));
        availableDirections.Add(Vector2.zero);

        StartCoroutine(ChangeDirection());
    }

    // Update is called once per frame
    void Update() {
        rb.velocity = currentDirection * movementSpeed;
    }

    IEnumerator ChangeDirection() {
        while (true) {
            
            currentDirection = availableDirections[Random.Range(0, availableDirections.Count)];
            transform.localScale = new Vector3(currentDirection.x == 0 ? transform.localScale.x : -currentDirection.x * 0.5f, transform.localScale.y);
            yield return new WaitForSeconds(Random.Range(changeDirectionDelayRange.x, changeDirectionDelayRange.y));
        }
    }

    public void ResetDir()
    {
        StopCoroutine(ChangeDirection());
        StartCoroutine(ChangeDirection());
    }

    public void Stop() {

        StopAllCoroutines();
        currentDirection = Vector2.zero;
        isStop = true;
    }

    public void Resume()
    {
        if (isStop)
        {
            StartCoroutine(ChangeDirection());
            isStop = false;
        }   
       
    }
    /*   
       Transform wanderTarget;
       Rigidbody2D rb;

       public Vector2 wanderRotateRange;
       float wanderRotateSpeed;

       // Use this for initialization
       void Start () {
           wanderTarget = transform.FindChild("Wander Target");
           rb = GetComponent<Rigidbody2D>();

           wanderRotateSpeed = Random.Range(wanderRotateRange.x, wanderRotateRange.y);
       }

       // Update is called once per frame
       void Update () {

           wanderTarget.RotateAround(transform.position, new Vector3(0, 0, 1), wanderRotateSpeed);

           Vector2 vecToTarget = wanderTarget.localPosition.normalized;

           rb.velocity = vecToTarget;

       }
       */
}
