using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour,IInteractable
{
    [SerializeField] string promptText;
    [SerializeField] UnityEvent onInteraction;

    public string GetDescription()
    {
        return promptText;
    }

    public void Interact()
    {
        if (enabled)
        {
            onInteraction?.Invoke();
        }
    }

    void OnDisable()
    {
        onInteraction.RemoveAllListeners();
    }
}
