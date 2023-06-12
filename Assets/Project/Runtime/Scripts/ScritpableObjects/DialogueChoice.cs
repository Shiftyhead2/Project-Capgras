using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New dialogue choice", menuName = "Data/Dialogue/New Choice", order = 0)]
public class DialogueChoice : ScriptableObject
{
    public string choiceText;
    public int ID;
}
