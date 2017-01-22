using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuScript : MonoBehaviour {


    public GameObject Waver;
    public GameObject Slapper;
    public GameObject MiddlePoint;
    public GameObject StartPanel;
    public GameObject WaSlapPanel;

    private Rigidbody2D m_WaverRB;
    private Rigidbody2D m_SlapperRB;
	// Use this for initialization
	void Start () {
        m_SlapperRB = Slapper.GetComponent<Rigidbody2D>();
        m_WaverRB = Waver.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (HighFive())
            StartPanel.SetActive(true);
	}

    bool HighFive()
    {
        float m_step = 2 * Time.fixedDeltaTime;
        Slapper.transform.position = Vector2.MoveTowards(Slapper.transform.position, MiddlePoint.transform.position, m_step);
        Waver.transform.position = Vector2.MoveTowards(Waver.transform.position, MiddlePoint.transform.position, m_step);
        if((Slapper.transform.position == MiddlePoint.transform.position) && (Waver.transform.position == MiddlePoint.transform.position))
        {
            WaSlapPanel.SetActive(false);
            return true;
        }

        return false;
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene(1);
        
    }
}
