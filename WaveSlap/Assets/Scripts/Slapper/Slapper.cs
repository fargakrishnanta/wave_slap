using UnityEngine;

using System.Collections;

public class Slapper : MovementControl {

    private GameObject Slappy;
    private Rigidbody2D m_rb;
    [SerializeField]
    private float m_speed = 1;
    private Vector3 m_target;
    private bool m_isSet;

    private Animator animator;

	// Use this for initialization
	void Start () {
        
        Slappy = this.gameObject;
        m_rb = this.gameObject.GetComponent<Rigidbody2D>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;

        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        
        FollowMouse();
	}


    void FollowMouse()
    {
        //get mouse position
        m_target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //set Z coord so the obj wont go up
        m_target.z = transform.position.z;
        //move it! :)
        //transform.position = Vector3.MoveTowards(transform.position, m_target, m_speed * Time.fixedDeltaTime);

        Vector3 deltaVel = (m_target - transform.position);

        m_rb.velocity = deltaVel.normalized * m_speed;

        FlipIt(deltaVel);

        if(m_rb.velocity.magnitude > 0.01f) {
            animator.SetBool("Moving", true);
        }
        else {
            animator.SetBool("Moving", false);
        }
    }
}
