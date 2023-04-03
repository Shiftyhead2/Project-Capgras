using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IDUIField : MonoBehaviour,IPointerClickHandler
{
    public int fieldID = 0;

    public TextMeshProUGUI fieldText;

    [SerializeField]
    private string fieldValueText;
    [SerializeField]
    private Image image;
    [SerializeField]
    private bool selected = false;

    private void OnEnable()
    {
        image.fillAmount = 0f;
        selected = false;
        GameEvents.onUpdateIDFields += UpdateField;
        GameEvents.onExitDetectiveMode += UnSelect;
        GameEvents.onDetectiveModalClosed += UnSelect;
    }

    private void OnDisable()
    {
        GameEvents.onUpdateIDFields -= UpdateField;
        GameEvents.onExitDetectiveMode -= UnSelect;
        GameEvents.onDetectiveModalClosed -= UnSelect;
    }


    void UpdateField(int ID, string fieldValue)
    {
        if(fieldID != ID)
        {
            return;
        }

        switch (ID)
        {
            case 0:
                //Gender
                fieldText.text = $"Gender: {fieldValue}";
                break;
            case 1:
                //First name
                fieldText.text = $"First name: {fieldValue}";
                break;
            case 2:
                //Last Name
                fieldText.text = $"Last name: {fieldValue}";
                break;
            default:
                break;

        }

        fieldValueText = fieldValue;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (DetectiveModeManager.inDetectiveMode && eventData.clickCount == 2)
        {
            Select();
            if (selected)
            {
                GameEvents.onPassField?.Invoke(fieldID, fieldValueText);
            }
            else
            {
                GameEvents.onUnselect?.Invoke(fieldID, fieldValueText);
            }
        }
    }

    void Select()
    {
        selected = !selected;
        TweenImage();
    }

    void UnSelect()
    {
        selected = false;
        GameEvents.onUnselect?.Invoke(fieldID, fieldValueText);
        TweenImage();
    }

    void TweenImage()
    {
        if (selected)
        {
            image.DOFillAmount(1f, 0.5f);
        }
        else
        {
            image.DOFillAmount(0f, 0.5f);
        }
    }
}
