using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelEnabler : MonoBehaviour
{
    private CanvasGroup canvasGroup;


    private async void Awake()
    {
        await WaitForCanvasGroupAsync();

        canvasGroup = GetComponent<CanvasGroup>();
    }

    public async void EnableThisUI()
    {
        await WaitForCanvasGroupAsync();

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        transform.SetAsLastSibling();
    }

    public async void DisableThisUI()
    {
        await WaitForCanvasGroupAsync();

        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    private async UniTask WaitForCanvasGroupAsync()
    {
        await AsyncWaitForComponents.WaitForComponentAsync<CanvasGroup>(gameObject);
    }
}
