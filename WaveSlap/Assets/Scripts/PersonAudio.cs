using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(AudioSource))]
public class PersonAudio : MonoBehaviour {

    //List of Audio associated with a person (waver/npcs0
    //- Wave 1
    //- Wave 2
    //- Footsteps

    public List<AudioClip> audioFiles;
    public List<string> audioKeys;

    public AudioSource audioSource;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        if (audioFiles.Count != audioKeys.Count) Debug.LogError("PersonAudio: Keys Files Count Mismatch");
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void PlaySound(string key) {
        audioSource.PlayOneShot(audioFiles[audioKeys.IndexOf(key)]);
    }
}
