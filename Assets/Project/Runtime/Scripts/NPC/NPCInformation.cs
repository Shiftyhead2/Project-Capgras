using UnityEngine;

public class NPCInformation : MonoBehaviour
{



    [field: SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    public string Name { get; private set; }

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

    public StatusScriptableObject currentStatus { get; private set; }

    [field: SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    public int daysSinceUpdate { get; private set; }


    public float begChance = 0.5f;
    public float bribeChance = 0.5f;

    private void Start()
    {
        GenerateInformation();
    }

    


    void GenerateInformation()
    {
        GetGender();
        GetName();
        isDoppleganger = IsDoppleganger();
        setUpStatus();
    }

    private void OnEnable()
    {
        GameEvents.onSearchFinished += getCurrentStatus;
        GameEvents.onGetName += getName;
        GameEvents.onPersonInformationGenerationDone += setUpDays;
        GameEvents.onGetDaysSinceUpdate += getDaysSinceUpdate;
    }

    private void OnDisable()
    {
        GameEvents.onSearchFinished -= getCurrentStatus;
        GameEvents.onGetName -= getName;
        GameEvents.onPersonInformationGenerationDone -= setUpDays;
        GameEvents.onGetDaysSinceUpdate -= getDaysSinceUpdate;
    }

    void GetName()
    {
        Name = GameEvents.onNameGenerated?.Invoke(Gender, false, Gender);
        GameEvents.onUpdateIDFields?.Invoke(1, Name);
    }



    void GetGender()
    {
        Gender = GameEvents.onGenderGenerated?.Invoke();
        GameEvents.onUpdateIDFields?.Invoke(0, Gender);
    }

    bool IsDoppleganger()
    {
        float chance = Random.Range(0f, 1f);
        return chance <= 0.3f;
    }

    void setUpDays(int currentAmountOfFalseData)
    {
        if(currentStatus.ID == 103)
        {
            daysSinceUpdate = -1;
            return;
        }


        if (!isDoppleganger)
        {
            if(currentAmountOfFalseData == 0)
            {
                daysSinceUpdate = Random.Range(0, 8);
            }
            else
            {
                daysSinceUpdate = Random.Range(0, 32);
            }
        }
        else
        {
            daysSinceUpdate = Random.Range(0, 8);
        }
        
    }

    void setUpStatus()
    {
        currentStatus = GameEvents.onStatusGenerated?.Invoke(isDoppleganger);
    }

    StatusScriptableObject getCurrentStatus()
    {
        return currentStatus;
    }

    string getName()
    {
        return Name;
    }

    int getDaysSinceUpdate()
    {
        return daysSinceUpdate;
    }

}
