using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallPerson : MonoBehaviour, IInteractionObject
{
    public void OnInteraction()
    {
        if (!GameManager.instance.ProcessingPerson)
        {
            GameEvents.onCallPerson?.Invoke();
        }
    }
}
