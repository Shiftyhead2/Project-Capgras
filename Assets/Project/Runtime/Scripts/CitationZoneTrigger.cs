using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitationZoneTrigger : MonoBehaviour
{
    [SerializeField]
    private int layer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == layer)
        {
            GameEvents.onCitationZoneEnter?.Invoke();
        }
    }
}
