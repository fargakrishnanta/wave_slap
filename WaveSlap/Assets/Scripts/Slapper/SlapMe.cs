using UnityEngine;
using System.Collections;

public class SlapMe : MonoBehaviour {

    [SerializeField]
    public float Boundary;

    [SerializeField]
    public int MaxSlapCount;

    public GameObject RightSlapPanel;
    public GameObject WrongSlapPanel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            SummonDaRay();
        }
	}

    /*Ray Cast dem slap mouse click*/
    void SummonDaRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            if (CheckInBoundary(hit.collider.gameObject.transform.position))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Person"))
                {
               
                    SlapMePlease(true);
                }
                else 
                if( hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
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
            Debug.Log("SLAPPPPP MMMEEE");
            if (a)
                RightSlapPanel.SetActive(true);
            else
                WrongSlapPanel.SetActive(true);

        }
    }
}
