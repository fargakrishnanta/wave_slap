using UnityEngine;
using System.Collections;

public class SlappyDash : MonoBehaviour {

    private Rigidbody2D m_rb;
    private float m_cd;
    private Vector3 m_target;
    private float m_timer;
    public DashState dashState;

    [SerializeField]
    public float DashSpeed;
    [SerializeField]
    public float MaxDuration;
    [SerializeField]
    public float CoolDown;

    private float m_cdTimer;

	// Use this for initialization
	void Start () {
        m_rb = this.gameObject.GetComponent<Rigidbody2D>();
        m_cdTimer = CoolDown;
	}
	
	// Update is called once per frame
	void Update () {

        switch (dashState)
        {
            //Ready
            case DashState.Ready:
                if (Input.GetMouseButtonDown(1))
                {
                    //LetsDash();
                    dashState = DashState.Dashing;

                    //spawnParticles
                }
                break;
            //Lets Dash Baby!
            case DashState.Dashing:
                m_timer += Time.fixedDeltaTime;
                LetsDash();
                if(m_timer >= MaxDuration)
                {
                    m_timer = 0;
                    dashState = DashState.CoolDown;
              
                }
                break;
            //Bruh, take a break. Stop spamming it. ;)
            case DashState.CoolDown:
                m_cdTimer -= Time.fixedDeltaTime;

                if(m_cdTimer <= 0)
                {
                    m_cdTimer = CoolDown;
                    dashState = DashState.Ready;
                }
                break;
        }
  
	}
    /* Leggo DASH!! */
    private void LetsDash()
    {
        m_target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //set Z coord so the obj wont go up
        m_target.z = transform.position.z;
        //move it! :)
        transform.position = Vector3.MoveTowards(transform.position, m_target, DashSpeed * Time.fixedDeltaTime);
    }
}

public enum DashState
{
    Ready,
    Dashing,
    CoolDown
}
