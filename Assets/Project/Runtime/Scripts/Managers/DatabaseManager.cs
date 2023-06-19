using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DatabaseManager : MonoBehaviour
{

    private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

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
        GameEvents.onAgeGenerated += GetAge;
        GameEvents.onWeightGenerated += GetWeight;
        GameEvents.onHeightGenerated += GetHeight;
        GameEvents.onBiometricIDGenerated += GetRandomString;
    }

    private void OnDisable()
    {
        GameEvents.onNameGenerated -= GetName;
        GameEvents.onGenderGenerated -= GetGender;
        GameEvents.onStatusGenerated -= ReturnStatusScriptableObject;
        GameEvents.onAgeGenerated -= GetAge;
        GameEvents.onWeightGenerated -= GetWeight;
        GameEvents.onHeightGenerated -= GetHeight;
        GameEvents.onBiometricIDGenerated -= GetRandomString;
    }


    string GetName(string gender, bool isFalse, string actualGender)
    {
        

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

    string GetAge()
    {
        return Random.Range(21, 71).ToString();
    }

    string GetWeight()
    {
        return Random.Range(48, 201).ToString();
    }

    int GetHeight()
    {
        return Random.Range(161, 176);
    }

    string GetRandomString(bool isFalse)
    {

        if (isFalse)
        {
            //Do something else
            return regenerateRandomString(GameEvents.onGetOriginalBiometricIDValue?.Invoke(), Random.Range(1, 5));
        }


        return getRandomString(9);

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

    string getRandomString(int length)
    {
        string generatedString = string.Empty;
        for (int i = 0; i < length; i++)
        {
            int randomIndex = Random.Range(0, Characters.Length);
            generatedString += Characters[randomIndex];
        }
        return generatedString;
    }

    string regenerateRandomString(string originalString, int numSteps)
    {
        string regeneratedString = originalString;
        int steps = Mathf.Min(numSteps, originalString.Length);

        for (int i = 0; i < steps; i++)
        {
            int randomIndex = Random.Range(0, originalString.Length);
            char newChar = Characters[Random.Range(0, Characters.Length)];
            regeneratedString = regeneratedString.Remove(randomIndex, 1).Insert(randomIndex, newChar.ToString());
        }

        return regeneratedString;

    }

}
