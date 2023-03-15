using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIDData : MonoBehaviour
{
    private NPCInformation informatiton;

    [SerializeField]
    private List<FieldData> IdFields = new List<FieldData>();
    [Header("Data Settings")]
    [SerializeField]
    private int amountOfFields = 1;
    [SerializeField]
    private int maxAmountOfFalseData = 2;

#if UNITY_EDITOR
    [field:SerializeField]
    [field:ReadOnlyInspector]
    public int currentAmountOfFalseData { get; private set; } = 0;
#endif
    [SerializeField]
    private bool isSuspicious;

    private void OnEnable()
    {
        GameEvents.onCitationZoneEnter += GetData;
        GameEvents.onSuspiciousFlag += flagSuspicious;
    }


    private void OnDisable()
    {
        GameEvents.onCitationZoneEnter -= GetData;
        GameEvents.onSuspiciousFlag -= flagSuspicious;
    }


    // Start is called before the first frame update
    void Start()
    {
        informatiton = GetComponent<NPCInformation>();
        currentAmountOfFalseData = 0;
        GenerateFields();
    }


    void GenerateFields()
    {
        for (int i = 0; i < amountOfFields; i++)
        {
            int id = i;
            bool isCorrect = isFalse();
            FieldData data = new FieldData(id, getFieldName(id), getValue(isCorrect,id), isCorrect);
            GameEvents.onUpdateBiometricFields(data);
            IdFields.Add(data);
        }
    }


    bool isFalse()
    {
        if(currentAmountOfFalseData >= maxAmountOfFalseData)
        {
            return false;
        }

        float percentChange = 10f;
        float randomValue = Random.Range(0f, 100f);
        
        if(randomValue > percentChange)
        {
            return false;
        }
        currentAmountOfFalseData++;
        return true;
    }

    string getFieldName(int id)
    {
        switch (id)
        {
            case 0:
                return "First Name";
            case 1:
                return "Last Name";
            case 2:
                return "Gender";
            default:
                return "Unknown";
        }
    }

    string getValue(bool isFalse, int id)
    {
        switch (id)
        {
            case 0:
                //First name
                if (isFalse)
                {
                    return "Incorrect";
                }
                return informatiton.FirstName;
            case 1:
                //Last name
                if (isFalse)
                {
                    return "Incorrect";
                }
                return informatiton.LastName;
            case 2:
                //Gender
                if (isFalse)
                {
                    if(informatiton.Gender.ToLower() == "male")
                    {
                        return "Female";
                    }
                    else
                    {
                        return "Male";
                    }
                }
                return informatiton.Gender;
            default:
                return "Unknown";
        }
    }

    void GetData()
    {
        GameEvents.onProcessFieldData?.Invoke(IdFields,isSuspicious);
    }

    void flagSuspicious(bool suspicious)
    {
        isSuspicious = suspicious;
    }
}
