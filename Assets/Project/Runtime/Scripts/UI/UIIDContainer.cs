using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UIIDContainer : MonoBehaviour
{
    private bool selected = false;
    private Image image;

    [SerializeField]
    private Color transparentColor;

    private void Awake()
    {
        selected = false;
        image = GetComponent<Image>();
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
            image.DOFade(0.5f, 0.5f);
        }
        else
        {
            image.DOFade(0f, 0.5f);
        }
    }

    void DisableUI()
    {
        selected = false;
        image.color = transparentColor;
    }

}
