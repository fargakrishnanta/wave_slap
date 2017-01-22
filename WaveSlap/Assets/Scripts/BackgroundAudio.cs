using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundAudio : MonoBehaviour {


    //List of Audio associated with background music
    //- Wave Slap - Default
    //- Wave Slap - Level 1
    //- Wave Slap - Max Happiness

    public List<AudioClip> audioFiles;
    public List<string> audioKeys;

    public AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioFiles.Count != audioKeys.Count) Debug.LogError("BackgroundAudio: Keys Files Count Mismatch");
    }

    public void PlaySound(string key, bool loopSound)
    {
        audioSource.clip = audioFiles[audioKeys.IndexOf(key)];
        if (audioSource.isPlaying == false)
        {
            audioSource.loop = loopSound;
            audioSource.Play();
        }
        
    }
    public void StopSound(string key)
    {
        audioSource.clip = audioFiles[audioKeys.IndexOf(key)];

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
