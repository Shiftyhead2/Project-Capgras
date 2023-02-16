using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryZoneScript : MonoBehaviour
{

    [SerializeField]
    private int layer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == layer)
        {
            GameEvents.onProcessPerson?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == layer)
        {
            GameEvents.onProcessPerson?.Invoke(false);
        }
    }
}
