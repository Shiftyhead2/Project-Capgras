using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryZoneScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out INPC npc))
        {
            GameEvents.onProcessPerson?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out INPC npc))
        {
            GameEvents.onProcessPerson?.Invoke(false);
        }
    }
}
