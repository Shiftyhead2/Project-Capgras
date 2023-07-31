using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;


public class UIDragable : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler
{

    private RectTransform rectTransform;
    private Canvas canvas;

    async void Awake()
    {
        await UniTask.WhenAll(
                WaitForRectTransformAsync(),
                WaitForCanvasAsync()
            );
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling();

    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
    
    private async UniTask WaitForRectTransformAsync()
    {
        await UniTask.WaitUntil(() => GetComponent<RectTransform>() != null);
        rectTransform = GetComponent<RectTransform>();
    }

    private async UniTask WaitForCanvasAsync()
    {
        await UniTask.WaitUntil(() => GetComponentInParent<Canvas>() != null);
        canvas = GetComponentInParent<Canvas>();
    }

   
}
