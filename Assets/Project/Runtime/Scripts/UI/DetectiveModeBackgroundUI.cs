using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class DetectiveModeBackgroundUI : MonoBehaviour
{

    private bool selected = false;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void OnEnable()
    {
        GameEvents.onEnterDetectiveMode += OnSelected;
        GameEvents.onExitDetectiveMode += OnDeselect;
        GameEvents.onNPCFullyChecked += DisableUI;
    }

    private void OnDisable()
    {
        GameEvents.onExitDetectiveMode -= OnDeselect;
        GameEvents.onEnterDetectiveMode -= OnSelected;
        GameEvents.onNPCFullyChecked -= DisableUI;
    }

    void OnSelected()
    {
        selected = !selected;
        TwerpImage();
    }

    void OnDeselect()
    {
        selected = false;
        TwerpImage();
    }


    void TwerpImage()
    {
        if (selected)
        {
            BlockRaycasts(true);
            canvasGroup.DOFade(0.5f, 0.5f);
        }
        else
        {
            
            canvasGroup.DOFade(0f, 0.5f).OnComplete(() => BlockRaycasts(false));
        }
    }


    void DisableUI()
    {
        selected = false;
        TwerpImage();
    }

    void BlockRaycasts(bool block)
    {
        canvasGroup.blocksRaycasts = block;
        canvasGroup.interactable = block;
    }
}
