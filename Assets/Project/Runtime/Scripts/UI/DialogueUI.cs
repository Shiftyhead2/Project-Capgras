using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI dialogueText;
    public Button[] buttons;
    public List<TextMeshProUGUI> buttonTexts;

    private void Start()
    {
        foreach(var button in buttons)
        {
            buttonTexts.Add(button.GetComponentInChildren<TextMeshProUGUI>());
        }
        HideUI();
    }


    private void OnEnable()
    {
        GameEvents.onNPCSituation += SetUpDialogue;
        GameEvents.onMicrophoneInteracted += ShowUI;
        GameEvents.onSituationResolved += HideUI;
    }

    private void OnDisable()
    {
        GameEvents.onNPCSituation -= SetUpDialogue;
        GameEvents.onMicrophoneInteracted -= ShowUI;
        GameEvents.onSituationResolved -= HideUI;
    }


    void SetUpDialogue(SituationObject situationObject)
    {
        dialogueText.text = situationObject.dialogueTexts[Random.Range(0,situationObject.dialogueTexts.Length-1)].text;

        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        for(int i = 0; i < situationObject.dialogueChoices.Length; i++)
        {
            buttons[i].gameObject.SetActive(true);
            buttonTexts[i].text = situationObject.dialogueChoices[i].choiceText;
        }
    }

    void HideUI()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        GameEvents.onHideMouse?.Invoke();
        GameEvents.onEnablePlayerInput?.Invoke();
    }

    void ShowUI()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        GameEvents.onShowMouse?.Invoke();
        GameEvents.onDisablePlayerInput?.Invoke();
    }

    public void ChoiceMade(int choice)
    {
        GameEvents.onSituationChoice?.Invoke(choice);
    }
}
