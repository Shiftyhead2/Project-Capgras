using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class CitzensDatabaseUI : MonoBehaviour
{
    [SerializeField] private GameObject searchPanel;
    [SerializeField] private GameObject searchingPanel;
    [SerializeField] private GameObject statusPanel;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image fillImage;

    [SerializeField] private TextMeshProUGUI searchNameText;
    [SerializeField] private TextMeshProUGUI citizenNameText;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private TextMeshProUGUI daysSinceUpdateText;

    [SerializeField] private float minWaitTime = 1f;
    [SerializeField] private float maxWaitTime = 5f;

    private string citizensName;

    private Coroutine searchCoroutine;

    private StatusScriptableObject referencedStatus;

    private void OnEnable()
    {
        GameEvents.onProcessPerson += GetCitizensName;
        GameEvents.onCallPerson += SetDefault;

        disableUI();
    }

    private void OnDisable()
    {
        GameEvents.onProcessPerson -= GetCitizensName;
        GameEvents.onCallPerson -= SetDefault;
    }


    public void disableUI()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        referencedStatus = null;

        StopAllCoroutines();
    }

    public void enableUI()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true; 
    }

    void SetDefault()
    {
        searchPanel.SetActive(true);
        searchingPanel.SetActive(false);
        statusPanel.SetActive(false);
    }

    void GetCitizensName(bool isProcessingPerson)
    {
        if (isProcessingPerson)
        {
            citizensName = GameEvents.onGetName?.Invoke();
            SetUpCitizenNameTexts();
        }
    }


    public void SearchCitizen()
    {
        if(citizensName != string.Empty)
        {
            searchCoroutine = StartCoroutine(SearchCitizenCouritine());
        }
    }

    private IEnumerator SearchCitizenCouritine()
    {
        fillImage.fillAmount = 0f;
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        searchPanel.SetActive(false);
        searchingPanel.SetActive(true);
        Tween imageFillTween = fillImage.DOFillAmount(1f, waitTime);
        yield return imageFillTween.WaitForCompletion();
        searchingPanel.SetActive(false);
        statusPanel.SetActive(true);
        referencedStatus = GameEvents.onSearchFinished?.Invoke();
        SetUpStatusText();
    }

    void SetUpStatusText()
    {
        if (referencedStatus != null)
        {
            statusText.text = $"Status: {referencedStatus.returnStatus()}";
        }
    }

    void SetUpCitizenNameTexts()
    {
        searchNameText.text = $"{citizensName}";
        citizenNameText.text = $"Citizen: {citizensName}";
    }

}

