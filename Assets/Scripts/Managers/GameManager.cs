using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private void OnEnable()
    {
        GameEvents.onCallPerson += SpawnObject;
    }

    private void OnDisable()
    {
        GameEvents.onCallPerson -= SpawnObject;
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

    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private GameObject spawnPrefab;
    [field:SerializeField]
    [field:ReadOnlyInspector]
    public bool ProcessingPerson { get; private set; }
    

    public void SpawnObject()
    {
        ProcessingPerson = true;
        Instantiate(spawnPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }
}
