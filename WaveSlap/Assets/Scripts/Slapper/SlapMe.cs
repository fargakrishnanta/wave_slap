using UnityEngine;
using UnityEngine.UI;
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
    public GameObject gameManager;

    public bool InGame = true;

    public Image OneDot;
    public Image TwoDot;
    public Image ThreeDot;

    [SerializeField]
    private AudioClip m_slapSound;
    [SerializeField]
    private AudioClip m_successSound;
    [SerializeField]
    private AudioClip m_failSound;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if(InGame)
                SummonDaRay();
        }

        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            if (MaxSlapCount == 0)
            {
                gameManager.GetComponent<GameManager>().GameOver();
                return;
            }
                
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
        InGame = false;
        if(MaxSlapCount > 0)
        {
            MaxSlapCount--;
           
            if(m_slapSound) {
                audioSource.clip = m_slapSound;
                audioSource.Play();
            }

            if (a)
            {
                RightSlapPanel.SetActive(true);
                heartbutton.SetActive(true);
                DisableStuff();

                if (m_successSound) {
                    audioSource.clip = m_successSound;
                    audioSource.Play();
                }

            }
            else
            {
                WrongSlapPanel.SetActive(true);

                if (m_failSound) {
                    audioSource.clip = m_failSound;
                    audioSource.Play();
                }


                DisableStuff();
            }
        }

        switch (MaxSlapCount)
        {
            case 3:
                OneDot.enabled = true;
                TwoDot.enabled = true;
                ThreeDot.enabled = true;
                break;
            case 2:
                OneDot.enabled = true;
                TwoDot.enabled = true;
                ThreeDot.enabled = false;
                break;
            case 1:
                OneDot.enabled = true;
                TwoDot.enabled = false;
                ThreeDot.enabled = false;
                break;
            default:
                OneDot.enabled = false;
                TwoDot.enabled = false;
                ThreeDot.enabled = false;
               
                break;
        }
    }

    void DisableStuff()
    {
        InGame = false;
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
        InGame = true;
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
