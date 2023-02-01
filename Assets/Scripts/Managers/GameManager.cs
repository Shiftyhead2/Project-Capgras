using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private GameObject spawnPrefab;

    public void SpawnObject()
    {
        Instantiate(spawnPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }
}
