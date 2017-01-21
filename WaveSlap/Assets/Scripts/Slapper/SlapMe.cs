using UnityEngine;
using System.Collections;

public class SlapMe : MonoBehaviour {

    [SerializeField]
    public float Boundary;
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
        Debug.Log(ray.origin.ToString());
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Person"))
            {
                if (CheckInBoundary(hit.collider.gameObject))
                {
                    SlapMePlease();
                }
            }

        }
    }

    /*Check if you are within the range to slap*/
    bool CheckInBoundary(GameObject go)
    {
        if ((this.transform.position - go.transform.position).magnitude <= Boundary)
            return true;

        return false;
    }

    //TODO Slapping
    void SlapMePlease()
    {
        Debug.Log("SLAPPPPP ME");
    }
}
