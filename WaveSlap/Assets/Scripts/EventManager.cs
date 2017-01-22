using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

    //Events
    public event NPCEventHandler HappinessIncreased;
    public event NPCEventHandler HappinessDecreased;

    public void EM_HappinessIncreased()
    {
        if (HappinessIncreased != null)
        {
            HappinessIncreased(this, new NPCEventArgs());
        }
    }

    public void EM_HappinessDecreased()
    {
        if (HappinessDecreased != null)
        {
            HappinessDecreased(this, new NPCEventArgs());
        }
    }
}
