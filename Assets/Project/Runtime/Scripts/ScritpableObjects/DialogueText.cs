using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DialogueType
{
    BEG,
    BRIBE
}

[CreateAssetMenu(fileName = "New dialogue text", menuName = "Dialogue/New Dialogue", order = 0)]
public class DialogueText : ScriptableObject
{
    public string openingText;
    public DialogueType type;
    public BribeDialogueVariation[] openingBribeVariation;
    public BribeDialogueVariation[] closingBribeVariation;
    public int minBribeAmount;
    public int maxBribeAmount;

    public string SetUpText(int bribeAmount)
    {
        switch (type)
        {
            case DialogueType.BEG:
                return openingText;
            case DialogueType.BRIBE:
                return $"{openingText}\n{openingBribeVariation[Random.Range(0, openingBribeVariation.Length - 1)].text} {bribeAmount} credits {closingBribeVariation[Random.Range(0, closingBribeVariation.Length - 1)].text}";
            default:
                return openingText;
        }
    }
}
