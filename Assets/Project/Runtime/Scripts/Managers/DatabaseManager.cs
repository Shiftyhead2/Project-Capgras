using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{

  private List<NamesScript> _firstMaleNames = new List<NamesScript>();
  private List<NamesScript> _firstFemaleNames = new List<NamesScript>();
  private List<NamesScript> _surNames = new List<NamesScript>();

  private string _generatedFirstName;
  private string _generatedLastName;

  private void Awake()
  {
    GetFirstNames();
  }

  private void OnEnable()
  {
    GameEvents.onNameGenerated += GetName;
    GameEvents.onGenderGenerated += GetGender;
  }

  private void OnDisable()
  {
    GameEvents.onNameGenerated -= GetName;
    GameEvents.onGenderGenerated -= GetGender;
  }


  string GetName(string gender, bool isFalse, string actualGender)
  {
        float chance = 0.5f;
        float randomChance = Random.value;

        if (isFalse)
        {
            if (gender.ToLower() == actualGender.ToLower())
            {
                if (chance >= randomChance)
                {   
                    _generatedFirstName = GetFirstName(actualGender);
                }
                else
                {
                    _generatedLastName = GetLastName(true);
                }
            }
            else
            {
                _generatedFirstName = GetFirstName(gender);
                if (chance >= randomChance)
                {
                    _generatedLastName = GetLastName(true);
                }
            }
        }
        else
        {
            _generatedFirstName = GetFirstName(gender);
           _generatedLastName = GetLastName(false);
        }

        return $"{_generatedFirstName} {_generatedLastName}";
  }


  string GetFirstName(string gender)
  {
    if (gender.ToLower() == "male")
    {
      return ReturnNonDuplicatedMaleNames(_generatedFirstName);
    }

     return ReturnNonDuplicatedFemaleNames(_generatedFirstName);
  }

  string GetLastName(bool shouldGenerateNonDuplicate)
  {
    return ReturnNonDuplicatedSurNames(_generatedLastName,shouldGenerateNonDuplicate);
  }

  string GetGender()
  {
    int gender = Random.Range(0, 2);
    switch (gender)
    {
      case 0:
        return "Male";
      case 1:
        return "Female";
      default:
        return "Unknown";
    }
  }

  void GetFirstNames()
  {
    var maleNames = Resources.LoadAll<NamesScript>("Data/Names/FirstNames/Male");
    var femaleNames = Resources.LoadAll<NamesScript>("Data/Names/FirstNames/Female");
    var surNames = Resources.LoadAll<NamesScript>("Data/Names/Surnames");

    foreach (var maleName in maleNames)
    {
      _firstMaleNames.Add(maleName);
    }

    foreach (var femaleName in femaleNames)
    {
      _firstFemaleNames.Add(femaleName);
    }

    foreach (var surName in surNames)
    {
      _surNames.Add(surName);
    }
  }

    string ReturnNonDuplicatedMaleNames(string currentMaleName)
    {
        return _firstMaleNames.FirstOrDefault(name => name.nameText != currentMaleName)?.nameText;
    }

    string ReturnNonDuplicatedFemaleNames(string currentFemaleName)
    {
        return _firstFemaleNames.FirstOrDefault(name => name.nameText != currentFemaleName)?.nameText;
    }

    string ReturnNonDuplicatedSurNames(string currentLastName,bool shouldGenerateNonDuplicate)
    {
        if (shouldGenerateNonDuplicate)
        {
            return _surNames.FirstOrDefault(name => name.nameText != currentLastName)?.nameText;
        }

        return _surNames[Random.Range(0, _surNames.Count)].nameText;
    }

}
