using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[Serializable]
public struct FieldData
{
    [field: SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    public int _id { get; private set; }
    [field:SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    public string _fieldName { get; private set; }
    [field:SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    public string _value { get; private set; }
    [field:SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    public bool _isFalse { get; private set; }

    public FieldData(int id,string fieldName,string value,bool isFalse)
    {
        _id = id;
        _value = value;
        _fieldName = fieldName;
        _isFalse = isFalse;
    }
}
