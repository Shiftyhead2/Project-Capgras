using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelEnabler : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public void EnableThisUI()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        transform.SetAsLastSibling();
    }

    public void DisableThisUI()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}
