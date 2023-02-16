using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        GameEvents.onComputerInteraction += EnableUI;
        GameEvents.onNPCFullyChecked += DisableUI;
        DisableUI();
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
