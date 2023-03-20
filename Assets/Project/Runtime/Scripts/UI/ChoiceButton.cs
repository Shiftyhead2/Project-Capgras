using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoiceButton : MonoBehaviour
{
    private TextMeshProUGUI buttonText;
    private int id;

    private void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();

    }

    public void SetUpText(string text, int _id)
    {
        buttonText.text = text;
        id = _id;
    }

    public void MakeChoice()
    {
        GameEvents.onChoiceButtonClicked.Invoke(id);
    }

}
