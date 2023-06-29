using UnityEngine;
using UnityEngine.Localization.Settings;

public class NPCInformation : MonoBehaviour
{
    [SerializeField]
    private DynamicModelAdjuster modelAdjuster;


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
    public string Age { get; private set; }

    [field: SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    public string Weight { get; private set; }

    [field: SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    public string Height { get; private set; }

    private int _nonStringHeight;
    private int _genderID;

    [field: SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    public string BiometricID { get; private set; }





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

    private void Awake()
    {
        modelAdjuster = GetComponentInChildren<DynamicModelAdjuster>();
        GenerateInformation();
    }

    


    void GenerateInformation()
    {
        GetGender();
        GetName();
        GetAge();
        GetWeight();
        GetHeight();
        GetBioID();
        isDoppleganger = IsDoppleganger();
        setUpStatus();
    }

    private void OnEnable()
    {
        GameEvents.onSearchFinished += getCurrentStatus;
        GameEvents.onGetName += getName;
        GameEvents.onPersonInformationGenerationDone += setUpDays;
        GameEvents.onGetDaysSinceUpdate += getDaysSinceUpdate;
        GameEvents.onGetOriginalBiometricIDValue += getOriginalBiometricID;
        GameEvents.onGetGenderID += getGenderID;
    }

    private void OnDisable()
    {
        GameEvents.onSearchFinished -= getCurrentStatus;
        GameEvents.onGetName -= getName;
        GameEvents.onPersonInformationGenerationDone -= setUpDays;
        GameEvents.onGetDaysSinceUpdate -= getDaysSinceUpdate;
        GameEvents.onGetOriginalBiometricIDValue -= getOriginalBiometricID;
        GameEvents.onGetGenderID -= getGenderID;
    }

    void GetName()
    {
        Name = GameEvents.onNameGenerated?.Invoke(_genderID, false, _genderID);
        GameEvents.onUpdateIDFields?.Invoke(1, Name);
    }



    void GetGender()
    {
        _genderID = (int)GameEvents.onGenderGenerated?.Invoke();
        Gender = _genderID == 0 ? LocalizationSettings.StringDatabase.GetLocalizedString(LocatilazitionStrings.DYNAMIC_UI_TABLE_NAME,LocatilazitionStrings.GENDER_MALE_KEY) : LocalizationSettings.StringDatabase.GetLocalizedString(LocatilazitionStrings.DYNAMIC_UI_TABLE_NAME, LocatilazitionStrings.GENDER_FEMALE_KEY);
        GameEvents.onUpdateIDFields?.Invoke(0, Gender);
    }

    void GetAge()
    {
        Age = GameEvents.onAgeGenerated?.Invoke();
        GameEvents.onUpdateIDFields?.Invoke(2, Age);
    }

    void GetWeight()
    {
        Weight = GameEvents.onWeightGenerated?.Invoke();
        GameEvents.onUpdateIDFields?.Invoke(3, Weight);
    }

    void GetHeight()
    {
        _nonStringHeight = (int)GameEvents.onHeightGenerated?.Invoke();
        Height = _nonStringHeight.ToString();
        GameEvents.onUpdateIDFields?.Invoke(4, Height);
        modelAdjuster.AdjustBaseOffset(_nonStringHeight);
    }

    void GetBioID()
    {
        BiometricID = GameEvents.onBiometricIDGenerated?.Invoke(false);
        GameEvents.onUpdateIDFields?.Invoke(5, BiometricID);
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

    string getOriginalBiometricID()
    {
        return BiometricID;
    }

    int getGenderID()
    {
        return _genderID;
    }

}
