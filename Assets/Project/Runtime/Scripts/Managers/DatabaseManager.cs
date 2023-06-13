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
    {;

        if (isFalse)
        {
            if (gender.ToLower() == actualGender.ToLower())
            {
                _generatedFirstName = GetFirstName(actualGender);
                _generatedLastName = GetLastName(true);
            }
            else
            {
                _generatedFirstName = GetFirstName(gender);
                _generatedLastName = GetLastName(true);
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
        return gender.ToLower() == "male"
            ? ReturnNonDuplicatedName(_firstMaleNames, _generatedFirstName)
            : ReturnNonDuplicatedName(_firstFemaleNames, _generatedFirstName);
    }

    string GetLastName(bool shouldGenerateNonDuplicate)
    {
        if (shouldGenerateNonDuplicate)
            return ReturnNonDuplicatedName(_surNames, _generatedLastName);

        return _surNames[Random.Range(0, _surNames.Count)].nameText;
    }

    string GetGender()
    {
        int gender = Random.Range(0, 2);
        return gender == 0 ? "Male" : "Female";
    }

    void GetNames()
    {
        Addressables.LoadAssetsAsync<NamesScript>(_femaleNameLabel, _firstFemaleNames.Add);
        Addressables.LoadAssetsAsync<NamesScript>(_maleNameLabel, _firstMaleNames.Add);
        Addressables.LoadAssetsAsync<NamesScript>(_surnameLabel, _surNames.Add);
    }

    private string ReturnNonDuplicatedName(List<NamesScript> namesList, string currentName)
    {
        return namesList.FirstOrDefault(name => name.nameText != currentName)?.nameText;
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
