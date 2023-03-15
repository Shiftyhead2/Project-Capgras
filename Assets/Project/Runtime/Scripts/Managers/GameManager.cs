using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private GameObject spawnPrefab;
    [field: SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    public bool ProcessingPerson { get; private set; }
    [field: SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    public bool SpawnedPerson { get; private set; }

    public bool NPCSituation { get; private set; } = false;

    private int approvedCount;
    private int citations;
    public int fine { get; private set; } = 5;

    private void OnEnable()
    {
        GameEvents.onCallPerson += SpawnObject;
        GameEvents.onProcessPerson += ProcessPerson;
        GameEvents.onAIWaypointReached += DespawnPerson;
        GameEvents.onNPCDocumentsChecked += IncreaseApprovedCount;
        GameEvents.onCitationGiven += IncreaseCitationCount;
        GameEvents.onNPCSituation += onNPCSituation;
        GameEvents.onSituationResolved += onNPCSituationResolved;
    }

    private void OnDisable()
    {
        GameEvents.onCallPerson -= SpawnObject;
        GameEvents.onProcessPerson -= ProcessPerson;
        GameEvents.onAIWaypointReached -= DespawnPerson;
        GameEvents.onNPCDocumentsChecked -= IncreaseApprovedCount;
        GameEvents.onCitationGiven -= IncreaseCitationCount;
        GameEvents.onNPCSituation -= onNPCSituation;
        GameEvents.onSituationResolved -= onNPCSituationResolved;
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }

        
    }

    private void Start()
    {
        approvedCount = 0;
        citations = 0;
        fine = 5;
        NPCSituation = false;
        UpdateText(approvedCount);
    }


    void IncreaseApprovedCount(bool approved)
    {
        if (approved)
        {
            approvedCount++;
            UpdateText(approvedCount);
        }
    }


    private void SpawnObject()
    {
        SpawnedPerson = true;
        Instantiate(spawnPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

    private void DespawnPerson()
    {
        SpawnedPerson = false;
    }

    private void onNPCSituation(SituationObject situationObject)
    {
        NPCSituation = true;
    }

    private void onNPCSituationResolved()
    {
        NPCSituation = false;
    }

    private void ProcessPerson(bool isInProcessing)
    {
        ProcessingPerson = isInProcessing;
    }

    private void UpdateText(int i)
    {
        GameEvents.onUpdateText?.Invoke(i);
    }

    private void IncreaseCitationCount()
    {
        citations++;
        if(citations % 4 == 0)
        {
            fine += 5;
        }
    }

}
