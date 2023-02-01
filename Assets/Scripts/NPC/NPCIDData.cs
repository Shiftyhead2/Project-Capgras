using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIDData : MonoBehaviour
{
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
        currentAmountOfFalseData = 0;
        GenerateFields();
    }


    void GenerateFields()
    {
        for (int i = 0; i < amountOfFields; i++)
        {
            int id = i;
            bool isCorrect = isFalse();
            FieldData data = new FieldData(id, getFieldName(id), getValue(isCorrect), isCorrect);
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
                return "Name";
            default:
                return "Unknown";
        }
    }

    string getValue(bool isFalse)
    {
        if (isFalse)
        {
            return "Incorrect";
        }

        return "Correct";
    }
}
