using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
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


    string GetFirstName()
    {
        return "FirstName";
    }

    string GetLastName()
    {
        return "LastName";
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
}
