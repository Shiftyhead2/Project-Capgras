using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IDUIField : MonoBehaviour
{
    public int fieldID = 0;

    public TextMeshProUGUI fieldText;

    private void OnEnable()
    {
        GameEvents.onUpdateIDFields += UpdateField;
    }

    private void OnDisable()
    {
        GameEvents.onUpdateIDFields -= UpdateField;
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
                //First name
                fieldText.text = $"First name: {fieldValue}";
                break;
            case 1:
                //Last name
                fieldText.text = $"Last name: {fieldValue}";
                break;
            case 2:
                fieldText.text = $"Gender: {fieldValue}";
                break;
            default:
                break;

        }
    }
}
