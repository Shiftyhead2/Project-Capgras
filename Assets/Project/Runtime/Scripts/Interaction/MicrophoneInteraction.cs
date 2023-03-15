using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneInteraction : MonoBehaviour, IInteractionObject
{
    public void OnInteraction()
    {
        if(GameManager.instance.ProcessingPerson && GameManager.instance.NPCSituation)
        {
            GameEvents.onMicrophoneInteracted?.Invoke();
        }
    }
}
