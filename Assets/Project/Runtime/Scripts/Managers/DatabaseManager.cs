using Codice.CM.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{

    private List<NamesScript> _firstMaleNames = new List<NamesScript>();
    private List<NamesScript> _firstFemaleNames = new List<NamesScript>();
    private List<NamesScript> _surNames = new List<NamesScript>();

    private void Awake()
    {
        GetFirstNames();
    }



    private void OnEnable()
    {
        GameEvents.onFirstNameGenerated += GetFirstName;
        GameEvents.onLastNameGenerated += GetLastName;
        GameEvents.onGenderGenerated += GetGender;
    }

    private void OnDisable()
    {
        GameEvents.onFirstNameGenerated -= GetFirstName;
        GameEvents.onLastNameGenerated -= GetLastName;
        GameEvents.onGenderGenerated -= GetGender;
    }


    string GetFirstName(string gender)
    {
        if(gender.ToLower() == "male")
        {
            return _firstMaleNames[Random.Range(0,_firstMaleNames.Count)].nameText;
        }

        return _firstFemaleNames[Random.Range(0, _firstFemaleNames.Count)].nameText;
    }

    string GetLastName()
    {
        return _surNames[Random.Range(0,_surNames.Count)].nameText;
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

        foreach(var maleName in maleNames)
        {
            _firstMaleNames.Add(maleName);
        }

        foreach(var femaleName in femaleNames)
        {
            _firstFemaleNames.Add(femaleName);
        }

        foreach(var surName in surNames)
        {
            _surNames.Add(surName);
        }
    }
}
