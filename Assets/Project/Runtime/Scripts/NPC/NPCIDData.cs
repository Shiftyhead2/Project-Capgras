using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for generating biometric field data
/// </summary>
public class NPCIDData : MonoBehaviour
{
    private NPCInformation npcInformation;

    [SerializeField]
    private List<FieldData> idFields = new List<FieldData>();

    [Header("Data Settings")]
    [SerializeField]
    private int amountOfFields = 1;
    [SerializeField]
    private int maxAmountOfFalseData = 2;

    [SerializeField]
    private bool isSuspicious;

    private string currentGender;

#if UNITY_EDITOR
    [field: SerializeField]
    [field: ReadOnlyInspector]
#endif
    public int CurrentAmountOfFalseData { get; private set; } = 0;

    private void OnEnable()
    {
        GameEvents.onCitationZoneEnter += GetData;
        GameEvents.onSuspiciousFlag += FlagSuspicious;
    }


    private void OnDisable()
    {
        GameEvents.onCitationZoneEnter -= GetData;
        GameEvents.onSuspiciousFlag -= FlagSuspicious;
    }


    // Start is called before the first frame update
    void Start()
    {
        npcInformation = GetComponent<NPCInformation>();
        CurrentAmountOfFalseData = 0;
        GenerateFields();
    }


    /// <summary>
    /// A function that generates the fields
    /// </summary>
    void GenerateFields()
    {
        for (int i = 0; i < amountOfFields; i++)
        {
            int id = i;
            bool isCorrect = IsFalse();
            FieldData data = GenerateFieldData(id,isCorrect);
            idFields.Add(data);
            GameEvents.onUpdateBiometricFields(data);
        }
    }

    /// <summary>
    /// A bool method that when calls randomly sets the field to be false or true
    /// </summary>
    /// <returns>A bool that can be either false or true</returns>
    bool IsFalse()
    {
        if (npcInformation.isDoppleganger || CurrentAmountOfFalseData >= maxAmountOfFalseData)
        {
            return false;
        }

        float percentChange = 50f;
        float randomValue = Random.Range(0f, 100f);

        if (randomValue > percentChange)
        {
            CurrentAmountOfFalseData++;
            return true;
        }

        return false;
    }

    private FieldData GenerateFieldData(int id, bool isCorrect)
    {
        string fieldName = GetFieldName(id);
        string fieldValue = GetValue(isCorrect, id);

        return new FieldData(id, fieldName, fieldValue, isCorrect);
    }

    /// <summary>
    /// A string method for returning the field name using the correct <paramref name="id" />
    /// </summary>
    /// <param name="id">the ID of the field being passed</param>
    /// <returns>A string value that represents the field name</returns>
    string GetFieldName(int id)
    {
        switch (id)
        {
            case 0:
                return "Gender";
            case 1:
                return "Name";
            default:
                return "Unknown";
        }
    }

    /// <summary>
    /// A string method that generates field values based on <paramref name= "id"/> of the field and if the field is false or not 
    /// </summary>
    /// <param name="isFalse">A passed bool that can be false or true.It's used to regenerate field values if the field is false</param>
    /// <param name="id">A passed ID of the field</param>
    /// <returns>A string containing the field value</returns>
    string GetValue(bool isFalse, int id)
    {
        switch (id)
        {
            case 0:
                //Gender
                if (isFalse)
                {
                    currentGender = npcInformation.Gender.ToLower() == "male" ? "Female" : "Male";
                }
                else
                {
                    currentGender = npcInformation.Gender;
                }
                return currentGender;
            case 1:
                if (isFalse)
                {
                    return GameEvents.onNameGenerated?.Invoke(currentGender, true, npcInformation.Gender) ?? npcInformation.Name;
                }
                return npcInformation.Name;
            default:
                return "Unknown";
        }
    }

    /// <summary>
    /// A method for getting the NPC ID data
    /// </summary>
    void GetData()
    {
        GameEvents.onProcessFieldData?.Invoke(idFields, isSuspicious, npcInformation.isDoppleganger);
    }


    /// <summary>
    /// A method for setting the isSuspicious variable 
    /// </summary>
    /// <param name="suspicious">A passed bool used for setting the isSuspicious variable</param>  
    void FlagSuspicious(bool suspicious)
    {
        isSuspicious = suspicious;
    }

}
