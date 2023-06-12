using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DatabaseManager : MonoBehaviour
{

    private List<NamesScript> _firstMaleNames = new List<NamesScript>();
    private List<NamesScript> _firstFemaleNames = new List<NamesScript>();
    private List<NamesScript> _surNames = new List<NamesScript>();
    [SerializeField] private List<StatusScriptableObject> _statusObjects = new List<StatusScriptableObject>();

    [SerializeField] private AssetLabelReference _femaleNameLabel;
    [SerializeField] private AssetLabelReference _surnameLabel;
    [SerializeField] private AssetLabelReference _maleNameLabel;


    private string _generatedFirstName;
    private string _generatedLastName;


    private void Awake()
    {
        GetNames();
    }

    private void OnEnable()
    {
        GameEvents.onNameGenerated += GetName;
        GameEvents.onGenderGenerated += GetGender;
        GameEvents.onStatusGenerated += ReturnStatusScriptableObject;
    }

    private void OnDisable()
    {
        GameEvents.onNameGenerated -= GetName;
        GameEvents.onGenderGenerated -= GetGender;
        GameEvents.onStatusGenerated -= ReturnStatusScriptableObject;
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
        return ReturnNonDuplicatedSurNames(_generatedLastName, shouldGenerateNonDuplicate);
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

    void GetNames()
    {
        Addressables.LoadAssetsAsync<NamesScript>(_femaleNameLabel, (femaleName) =>
        {
            _firstFemaleNames.Add(femaleName);
        });

        Addressables.LoadAssetsAsync<NamesScript>(_maleNameLabel, (maleName) => 
        {
            _firstMaleNames.Add(maleName);
        });

        Addressables.LoadAssetsAsync<NamesScript>(_surnameLabel, (surname) =>
        {
            _surNames.Add(surname);
        });
    }

    string ReturnNonDuplicatedMaleNames(string currentMaleName)
    {
        return _firstMaleNames.FirstOrDefault(name => name.nameText != currentMaleName)?.nameText;
    }

    string ReturnNonDuplicatedFemaleNames(string currentFemaleName)
    {
        return _firstFemaleNames.FirstOrDefault(name => name.nameText != currentFemaleName)?.nameText;
    }

    string ReturnNonDuplicatedSurNames(string currentLastName, bool shouldGenerateNonDuplicate)
    {
        if (shouldGenerateNonDuplicate)
        {
            return _surNames.FirstOrDefault(name => name.nameText != currentLastName)?.nameText;
        }

        return _surNames[Random.Range(0, _surNames.Count)].nameText;
    }

    StatusScriptableObject ReturnStatusScriptableObject(bool isDoppleganger)
    {
        if (isDoppleganger)
        {
            return _statusObjects[Random.Range(0, _statusObjects.Count)];
        }

        return _statusObjects[Random.Range(0, 2)];
    }

}
