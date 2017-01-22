using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Note.. How unfortunate, this code will look quite messy..
[System.Serializable]
public class GOAnimation {
    public string name;
    public List<GameObject> objectsToAnimate;

    public float timeToComplete;
    public float animationDelay; // For having multiple animations

    protected float currentTime;

    public enum AnimationType { Translate, Appear, Disappear };

    public AnimationType animationType;

    public bool isComplete;

    //Translation Animation 
    public Vector3 translateStartPoint;
    public Vector3 translateEndPoint;

    //Appear & Disappear Animation

    public virtual void InitializeAnim() {
        switch (animationType) {
            case AnimationType.Translate:
                foreach (GameObject obj in objectsToAnimate) {
                    obj.transform.position = translateStartPoint;
                }
                break;
            case AnimationType.Appear:
                foreach (GameObject obj in objectsToAnimate) {
                    //obj.SetActive(false);
                }
                break;
            case AnimationType.Disappear:
                foreach (GameObject obj in objectsToAnimate) {
                    //obj.SetActive(true);
                }
                break;
            default:
                break;
        }
    }

    public virtual void NextFrame(float deltaTime) {
        currentTime += deltaTime;
        
        if(currentTime >= animationDelay) {
            switch (animationType) {
                case AnimationType.Translate:
                    Vector3 currPos = translateStartPoint + (translateEndPoint - translateStartPoint) * Mathf.Min(currentTime-animationDelay, timeToComplete) / timeToComplete;

                    foreach (GameObject obj in objectsToAnimate) {
                        obj.transform.position = currPos;
                    }
                    break;
                case AnimationType.Appear:
                    //STRETCH add fade
                    foreach (GameObject obj in objectsToAnimate) {
                        obj.SetActive(true);
                    }
                    break;
                case AnimationType.Disappear:
                    //STRETCH add fade
                    foreach (GameObject obj in objectsToAnimate) {
                        obj.SetActive(false);
                    }
                    break;
                default:
                    break;
            }
        }

        if (currentTime >= timeToComplete + animationDelay) isComplete = true;
    }

    

    public virtual void Reset() {
        currentTime = 0;
    }
}
