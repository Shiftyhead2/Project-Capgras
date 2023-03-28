using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInformation : MonoBehaviour
{
    [field:SerializeField]
#if UNITY_EDITOR
    [field:ReadOnlyInspector]
#endif
    public string FirstName {get; private set; }
    [field:SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    public string LastName { get; private set;}
    [field: SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    public string Gender { get; private set; }

    [field: SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    public bool isDoppleganger { get; private set; }

    public SituationObject beggingObject;
    public SituationObject bribeObject;

    public float begChance = 0.5f;
    public float bribeChance = 0.5f;

    private void Start()
    {
        GenerateInformation();
    }


    void GenerateInformation()
    {
        GetFirstName();
        GetLastName();
        GetGender();
        isDoppleganger = IsDoppleganger();
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

    bool IsDoppleganger()
    {
        float chance = Random.Range(0f, 1f);
        return chance >= 0.5f;
    }

}
