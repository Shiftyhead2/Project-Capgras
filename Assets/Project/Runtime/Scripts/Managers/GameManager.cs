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

    private void OnEnable()
    {
        GameEvents.onCallPerson += SpawnObject;
        GameEvents.onProcessPerson += ProcessPerson;
    }

    private void OnDisable()
    {
        GameEvents.onCallPerson -= SpawnObject;
        GameEvents.onProcessPerson -= ProcessPerson;
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

   
    

    private void SpawnObject()
    {
        SpawnedPerson = true;
        Instantiate(spawnPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

    private void ProcessPerson(bool isInProcessing)
    {
        ProcessingPerson = isInProcessing;
    }
}
