using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInteraction : MonoBehaviour, IInteractionObject
{
    public void OnInteraction()
    {
        if (GameManager.instance.ProcessingPerson)
        {
            GameEvents.onComputerInteraction?.Invoke();
        }
    }
}
