using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FieldData
{
    [SerializeField]
    private int id;
    [SerializeField]
    private string fieldName;
    [SerializeField]
    private string value;
    [SerializeField]
    private bool isFalse;

    public FieldData(int id,string fieldName,string value,bool isFalse)
    {
        this.id = id;
        this.value = value;
        this.fieldName = fieldName;
        this.isFalse = isFalse;
    }
}
