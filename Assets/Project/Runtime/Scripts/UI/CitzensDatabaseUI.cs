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

    [SerializeField] private UIPanelEnabler panelEnabler;
    [SerializeField] private Image fillImage;

    [SerializeField] private TextMeshProUGUI searchNameText;
    [SerializeField] private TextMeshProUGUI citizenNameText;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private TextMeshProUGUI daysSinceUpdateText;

    [SerializeField] private float minWaitTime = 1f;
    [SerializeField] private float maxWaitTime = 5f;

    private string citizensName;
    private int daysSinceLastUpdate;

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
        panelEnabler.DisableThisUI();

        referencedStatus = null;
        daysSinceLastUpdate = 0;

        StopAllCoroutines();
    }

    public void enableUI()
    {
        panelEnabler.EnableThisUI();

        transform.SetAsLastSibling();
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
        daysSinceLastUpdate = (int)GameEvents.onGetDaysSinceUpdate?.Invoke();
        SetUpStatusText();
    }

    void SetUpStatusText()
    {
        if (referencedStatus != null)
        {
            statusText.text = $"{ReturnString(LocatilazitionStrings.DATABASE_STATUS_TEXT_KEY, new object[] {referencedStatus.returnStatus()})}";
        }

        daysSinceUpdateText.text = $"{ReturnString(LocatilazitionStrings.DATABASE_DAYS_SINCE_UPDATE_KEY, new object[] { daysSinceLastUpdate })}";
    }

    void SetUpCitizenNameTexts()
    {
        searchNameText.text = $"{citizensName}";
        citizenNameText.text = $"{ReturnString(LocatilazitionStrings.DATABASE_CIVILIAN_TEXT_KEY, new object[] { citizensName })}";
    }


    string ReturnString(string string_key, object[] args = null)
    {
        return GetLocalizedText(LocatilazitionStrings.DYNAMIC_UI_TABLE_NAME, string_key, args);
    }

    string GetLocalizedText(string table_key, string string_key, object[] args = null)
    {
        return LocalizationEventManager.GetLocalizedString(table_key, string_key, args);
    }

}

