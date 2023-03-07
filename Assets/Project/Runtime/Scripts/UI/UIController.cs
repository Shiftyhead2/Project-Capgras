using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        DisableUI();
    }

    private void OnEnable()
    {
        
        GameEvents.onComputerInteraction += EnableUI;
        GameEvents.onNPCFullyChecked += DisableUI;
        
    }

    private void OnDisable()
    {
        GameEvents.onComputerInteraction -= EnableUI;
        GameEvents.onNPCFullyChecked -= DisableUI;
    }

    void DisableUI()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }




    void EnableUI()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

}
