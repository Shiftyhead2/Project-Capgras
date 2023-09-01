using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BiometricUIField : MonoBehaviour, IPointerClickHandler , IPointerEnterHandler, IPointerExitHandler
{
    public int fieldID = 0;
    public TextMeshProUGUI fieldText;
    [SerializeField]
    private string fieldValue;
    [SerializeField]
    private Image fillImage;
    [SerializeField]
    private bool selected = false;
    [SerializeField]
    private Image bgImage;

    [SerializeField]
    private Color hoveredColor;
    [SerializeField]
    private Color unhoveredColor;

    private void OnEnable()
    {
        fillImage.fillAmount = 0f;
        selected = false;
        bgImage.color = unhoveredColor;
        GameEvents.onUpdateBiometricFields += UpdateBiometricField;
        GameEvents.onExitDetectiveMode += UnSelect;
        GameEvents.onExitDetectiveMode += UnHover;
        GameEvents.onDetectiveModalClosed += UnSelect;
    }

    private void OnDisable()
    {
        GameEvents.onUpdateBiometricFields -= UpdateBiometricField;
        GameEvents.onExitDetectiveMode -= UnSelect;
        GameEvents.onExitDetectiveMode -= UnHover;
        GameEvents.onDetectiveModalClosed -= UnSelect;
    }

    void UpdateBiometricField(FieldData data)
    {
        if (fieldID != data.Id)
        {
            return;
        }
        fieldValue = data.Value;

        if (fieldID == 3)
        {
            fieldText.text = $"{data.FieldName}: {data.Value} kg";
        }
        else if(fieldID == 4)
        {
            fieldText.text = $"{data.FieldName}: {data.Value} cm";
        }
        else
        {
            fieldText.text = $"{data.FieldName}: {data.Value}";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameEvents.onGetDetectiveMode?.Invoke() == null)
        {
            return;
        }



        if ((bool)GameEvents.onGetDetectiveMode?.Invoke() && eventData.clickCount == 2)
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
            fillImage.DOFillAmount(1f, 0.5f);
        }
        else
        {
            fillImage.DOFillAmount(0f, 0.5f);
        }
    }

    void UnHover()
    {
        TweenColorBackgroundImage(unhoveredColor);
    }

    void TweenColorBackgroundImage(Color color)
    {
        bgImage.DOColor(color, 0.5f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameEvents.onGetDetectiveMode?.Invoke() == null)
        {
            return;
        }

        if(!selected && (bool)GameEvents.onGetDetectiveMode?.Invoke())
        {
            TweenColorBackgroundImage(hoveredColor);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GameEvents.onGetDetectiveMode?.Invoke() == null)
        {
            return;
        }

        if ((bool)GameEvents.onGetDetectiveMode?.Invoke())
        {
            TweenColorBackgroundImage(unhoveredColor);
        }
    }
}
