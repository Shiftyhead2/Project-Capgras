using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColorRandomMization : MonoBehaviour
{
    //THIS IS LITERALLY JUST AN EXAMPLE SCRIPT



    MeshRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void ChangeColor()
    {
        mesh.material.color = new Color(Random.value, Random.value, Random.value);
    }
}
