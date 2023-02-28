using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitationZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out INPC npc))
        {
            GameEvents.onCitationZoneEnter?.Invoke();
        }
    }
}
