using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

    //Events
    public event NPCEventHandler HappinessIncreased;
    public event NPCEventHandler HappinessDecreased;
    public event NPCEventHandler NPCSpawned;
    public event NPCEventHandler WaverSpawned;
    public event NPCEventHandler InitialHordeSpawned;
    public event NPCEventHandler NPCRemoved;

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

    public void EM_NPC_Spawned(GameObject npc)
    {
        if (NPCSpawned != null)
        {
            NPCEventArgs spwnArgs = new NPCEventArgs();

            spwnArgs.gameObject = npc;

            NPCSpawned(this, spwnArgs);
        }
    }
    public void EM_Waver_Spawned()
    {
        if (WaverSpawned != null)
        {
            WaverSpawned(this, new NPCEventArgs());
        }
    }
    public void EM_InitialHorde_Spawned()
    {
        if (InitialHordeSpawned != null)
        {
            InitialHordeSpawned(this, new NPCEventArgs());
        }
    }
    public void EM_NPC_Removed()
    {
        if (NPCRemoved != null)
        {
            NPCRemoved(this, new NPCEventArgs());
        }
    }
}
