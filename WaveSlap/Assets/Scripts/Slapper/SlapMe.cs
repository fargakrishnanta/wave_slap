using UnityEngine;
using System.Collections;

public class SlapMe : MonoBehaviour {

    [SerializeField]
    public float Boundary;

    [SerializeField]
    public int MaxSlapCount;

    public GameObject RightSlapPanel;
    public GameObject WrongSlapPanel;

    public GameObject timerClass;
    public GameObject heartbutton;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            SummonDaRay();
        }

        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            WrongSlapPanel.SetActive(false);
            timerClass.GetComponent<CountdownTimer>().Resume();
            EnableStuff();
        }
	}

    /*Ray Cast dem slap mouse click*/
    void SummonDaRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask filter = LayerMask.GetMask("Person");
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, filter.value);
        if (hit.collider != null)
        {
            if (CheckInBoundary(hit.collider.gameObject.transform.position))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    SlapMePlease(true);
                }else
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Person"))
                {
               
                    SlapMePlease(false);
                }
               
            }

        }
    }

    /*Check if you are within the range to slap*/
    bool CheckInBoundary(Vector3 go)
    {
        if ((this.transform.position - go).magnitude <= Boundary)
            return true;

        return false;
    }

    //TODO Slapping
    void SlapMePlease(bool a)
    {
        
        if(MaxSlapCount > 0)
        {
            MaxSlapCount--;
           
            if (a)
            {
                RightSlapPanel.SetActive(true);
                heartbutton.SetActive(true);
                DisableStuff();
                
            }
            else
            {
                WrongSlapPanel.SetActive(true);
               
                DisableStuff();
            }
                

        }
    }

    void DisableStuff()
    {
        this.gameObject.GetComponent<Slapper>().enabled = false;
        this.gameObject.GetComponent<SlappyDash>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<WaverMovement>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<WaveController>().enabled = false;

        var ogs = GameObject.FindGameObjectsWithTag("NPC");
        foreach(var og in ogs)
        {
            og.GetComponent<NPCMovementControl>().Stop();
            og.GetComponent<WaveController>().Stop();
            og.gameObject.GetComponent<HappyController>().enabled = false;
            og.gameObject.GetComponent<WaveController>().enabled = false;

        }

        timerClass.GetComponent<CountdownTimer>().Pause();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             

    }

    void EnableStuff()
    {
        this.gameObject.GetComponent<Slapper>().enabled = true;
        this.gameObject.GetComponent<SlappyDash>().enabled = true;

        GameObject.FindGameObjectWithTag("Player").GetComponent<WaverMovement>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<WaveController>().enabled = true;

        var ogs = GameObject.FindGameObjectsWithTag("NPC");
        foreach (var og in ogs)
        {
            og.GetComponent<NPCMovementControl>().Resume();
            og.GetComponent<WaveController>().Resume();
            og.gameObject.GetComponent<HappyController>().enabled = true;
            og.gameObject.GetComponent<WaveController>().enabled = true;
        }
    }
}
