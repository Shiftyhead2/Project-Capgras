using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for generating biometric field data
/// </summary>
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
  [field: SerializeField]
  [field: ReadOnlyInspector]
#endif
    public int CurrentAmountOfFalseData { get; private set; } = 0;

  [SerializeField]
  private bool isSuspicious;

  private string currentGender;

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
    informatiton = GetComponent<NPCInformation>();
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
      FieldData data = new FieldData(id, GetFieldName(id), GetValue(isCorrect, id), isCorrect);
      GameEvents.onUpdateBiometricFields(data);
      IdFields.Add(data);
    }
  }

  /// <summary>
  /// A bool method that when calls randomly sets the field to be false or true
  /// </summary>
  /// <returns>A bool that can be either false or true</returns>
  bool IsFalse()
  {
    if (informatiton.isDoppleganger)
    {
      return false;
    }

    if (CurrentAmountOfFalseData >= maxAmountOfFalseData)
    {
      return false;
    }

    float percentChange = 50f;
    float randomValue = Random.Range(0f, 100f);

    if (randomValue > percentChange)
    {
      return false;
    }
    CurrentAmountOfFalseData++;
    return true;
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
          if (informatiton.Gender.ToLower() == "male")
          {
            currentGender = "Female";
            return "Female";
          }
          else
          {
            currentGender = "Male";
            return "Male";
          }
        }
        currentGender = informatiton.Gender;
        return informatiton.Gender;
      case 1:
        //First name
        if (isFalse)
        {
          var name = GameEvents.onNameGenerated?.Invoke(currentGender, isFalse, informatiton.Gender);
          return name;
        }
        return informatiton.Name;
      default:
        return "Unknown";
    }
  }

  /// <summary>
  /// A method for getting the NPC ID data
  /// </summary>
  void GetData()
  {
    GameEvents.onProcessFieldData?.Invoke(IdFields, isSuspicious, informatiton.isDoppleganger);
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
