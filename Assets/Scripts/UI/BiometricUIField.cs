using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BiometricUIField : MonoBehaviour, IPointerClickHandler
{
    public int fieldID = 0;
    public TextMeshProUGUI fieldText;
    [SerializeField]
    private string fieldValue;
    [SerializeField]
    private Image image;
    [SerializeField]
    private bool selected = false;

    private void OnEnable()
    {
        image.fillAmount = 0f;
        selected = false;
        GameEvents.onUpdateBiometricFields += UpdateBiometricField;
        GameEvents.onExitDetectiveMode += UnSelect;
        GameEvents.onModalClosed += UnSelect;
    }

    private void OnDisable()
    {
        GameEvents.onUpdateBiometricFields -= UpdateBiometricField;
        GameEvents.onExitDetectiveMode -= UnSelect;
        GameEvents.onModalClosed -= UnSelect;
    }

    void UpdateBiometricField(FieldData data)
    {
        if(fieldID != data._id)
        {
            return;
        }
        fieldValue = data._value;

        fieldText.text = $"{data._fieldName}: {data._value}";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
         if(DetectiveModeManager.inDetectiveMode && eventData.clickCount == 2)
        {
            Select();
            if (selected)
            {
                GameEvents.onPassField?.Invoke(fieldID, fieldValue);
            }
            else
            {
                GameEvents.onUnselect?.Invoke(fieldID, fieldValue);
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
        GameEvents.onUnselect?.Invoke(fieldID, fieldValue);
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
