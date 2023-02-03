using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInformation : MonoBehaviour
{
    [field:SerializeField]
    [field:ReadOnlyInspector]
    public string FirstName {get; private set; }
    [field:SerializeField]
    [field:ReadOnlyInspector]
    public string LastName { get; private set;}
    [field: SerializeField]
    [field: ReadOnlyInspector]
    public string Gender { get; private set; }

    private void Start()
    {
        GenerateInformation();
    }


    void GenerateInformation()
    {
        GetFirstName();
        GetLastName();
        GetGender();
       
    }

    void GetFirstName()
    {
        FirstName = GameEvents.onFirstNameGenerated?.Invoke();
        GameEvents.onUpdateIDFields?.Invoke(0, FirstName);
    }

    void GetLastName() 
    {
        LastName = GameEvents.onLastNameGenerated?.Invoke();
        GameEvents.onUpdateIDFields?.Invoke(1, LastName);
    }

    void GetGender()
    {
        Gender = GameEvents.onGenderGenerated?.Invoke();
        GameEvents.onUpdateIDFields?.Invoke(2,Gender);
    }

}
