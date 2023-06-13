using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<INPC>() != null)
        {
            GameEvents.onAIWaypointReached?.Invoke();
        }
    }
}
