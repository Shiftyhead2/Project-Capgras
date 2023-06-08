using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class IconUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private UnityEvent onClicked;
    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color highlightedColor;

    [SerializeField]
    private Image bgImage;

    private void Start()
    {
        bgImage.color = defaultColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TweenColor(highlightedColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TweenColor(defaultColor);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClicked.Invoke();
    }

    void TweenColor(Color color)
    {
        bgImage.DOColor(color, 0.5f);
    }

  
}
