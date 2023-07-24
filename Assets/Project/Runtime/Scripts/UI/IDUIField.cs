using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IDUIField : MonoBehaviour, IPointerClickHandler
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
        if (fieldID != ID)
        {
            return;
        }

        switch (ID)
        {
            case 0:
                //Gender
                fieldText.text = $"{GetLocalizedString(LocatilazitionStrings.DYNAMIC_UI_TABLE_NAME,LocatilazitionStrings.GENDER_FIELD_LOCALIZATION_KEY)}: {fieldValue}";
                break;
            case 1:
                //First name
                fieldText.text = $"{GetLocalizedString(LocatilazitionStrings.DYNAMIC_UI_TABLE_NAME, LocatilazitionStrings.NAME_FIELD_LOCALIZATION_KEY)}: {fieldValue}";
                break;
            case 2:
                fieldText.text = $"{GetLocalizedString(LocatilazitionStrings.DYNAMIC_UI_TABLE_NAME, LocatilazitionStrings.AGE_FIELD_LOCALIZATION_KEY)}: {fieldValue}";
                break;
            case 3:
                fieldText.text = $"{GetLocalizedString(LocatilazitionStrings.DYNAMIC_UI_TABLE_NAME, LocatilazitionStrings.WEIGHT_FIELD_LOCALIZATION_KEY)}: {fieldValue} kg";
                break;
            case 4:
                fieldText.text = $"{GetLocalizedString(LocatilazitionStrings.DYNAMIC_UI_TABLE_NAME, LocatilazitionStrings.HEIGHT_FIELD_LOCALIZATION_KEY)}: {fieldValue} cm";
                break;
            case 5:
                fieldText.text = $"{GetLocalizedString(LocatilazitionStrings.DYNAMIC_UI_TABLE_NAME, LocatilazitionStrings.BIOMETRICID_FIELD_LOCALIZATION_KEY)}: {fieldValue}";
                break;
            default:
                break;

        }

        fieldValueText = fieldValue;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(GameEvents.onGetDetectiveMode?.Invoke() == null)
        {
            return;
        }

        if ((bool)GameEvents.onGetDetectiveMode?.Invoke() && eventData.clickCount == 2)
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

    string GetLocalizedString(string table_key, string string_key)
    {
        return LocalizationEventManager.GetLocalizedString(table_key, string_key);
    }
}
