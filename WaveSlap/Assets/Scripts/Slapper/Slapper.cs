using UnityEngine;

using System.Collections;

public class Slapper : MonoBehaviour {

    private GameObject Slappy;
    private Rigidbody2D m_rb;
    private float m_speed = 1;
    private Vector3 m_target;
    private bool m_isSet;

	// Use this for initialization
	void Start () {
        
        Slappy = this.gameObject;
        m_rb = this.gameObject.GetComponent<Rigidbody2D>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
     
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
        transform.position = Vector3.MoveTowards(transform.position, m_target, m_speed * Time.fixedDeltaTime);

    }
}
