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
    [SerializeField]
    private int currentAmountOfFalseData = 0;


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

        float percentChange = 50f;
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
}
