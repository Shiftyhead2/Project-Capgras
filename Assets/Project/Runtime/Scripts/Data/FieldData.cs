using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

/// <summary>
/// A base struct for generating field data 
/// </summary>
[Serializable]
public struct FieldData
{
    /// <summary>
    /// The ID of the field
    /// </summary>
    [field: SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    public int Id { get; private set; }
    /// <summary>
    /// The name of the field
    /// </summary>
    [field: SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    public string FieldName { get; private set; }
    /// <summary>
    /// The value of the field
    /// </summary>
    [field: SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    public string Value { get; private set; }
    /// <summary>
    /// A bool if the field is false or not
    /// </summary>
    [field: SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    public bool IsFalse { get; private set; }

    /// <summary>
    /// A base constructor for the struct. It needs <paramref name = "id" />, <paramref name = "fieldName"/>, <paramref name = "value" /> and <paramref name = "isFalse" /> 
    /// to be correctly constructed
    /// </summary>
    /// <param name="id">The ID of the field passed as an int</param>
    /// <param name="fieldName">The name of the field passed as an string</param>
    /// <param name="value">The field value passed as an string</param>
    /// <param name="isFalse">The bool that will tell if the field is false or not</param>
    public FieldData(int id, string fieldName, string value, bool isFalse)
    {
        Id = id;
        Value = value;
        FieldName = fieldName;
        IsFalse = isFalse;
    }

    public void setFieldName(string value)
    {
        FieldName = value;
    }
}
