using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Situation Object", menuName = "Data/Dialogue/Situation Object", order = 0)]
public class SituationObject : ScriptableObject
{
    public DialogueText[] dialogueTexts;
    public DialogueChoice[] dialogueChoices;
}
