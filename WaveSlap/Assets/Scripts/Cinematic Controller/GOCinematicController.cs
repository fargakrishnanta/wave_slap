using UnityEngine;
using System.Collections.Generic;



public class GOCinematicController : MonoBehaviour {

    public List<GOAnimation> animationList;
    public List<GOAnimation> removeList;

    public float delay;


	// Use this for initialization
	void Start () {
        removeList = new List<GOAnimation>();


        foreach (GOAnimation anim in animationList) {
            anim.InitializeAnim();
        }
    }
	
	// Update is called once per frame
	void Update () {
	    foreach(GOAnimation anim in animationList) {
            anim.NextFrame(Time.deltaTime);

            if (anim.isComplete) {
                removeList.Add(anim);
            }
        }   

        foreach(GOAnimation anim in removeList) {
            animationList.Remove(anim);
        }
	}
}
