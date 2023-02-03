using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BiometricUIField : MonoBehaviour
{
    public int fieldID = 0;
    public TextMeshProUGUI fieldText;

    private void OnEnable()
    {
        GameEvents.onUpdateBiometricFields += UpdateBiometricField;
    }

    private void OnDisable()
    {
        GameEvents.onUpdateBiometricFields -= UpdateBiometricField;
    }

    void UpdateBiometricField(FieldData data)
    {
        if(fieldID != data._id)
        {
            return;
        }

        fieldText.text = $"{data._fieldName}: {data._value}";
    }
}
