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
        await UniTask.Yield(PlayerLoopTiming.Initialization);

        await UniTask.WhenAll(
                AsyncWaitForComponents.WaitForComponentAsync<RectTransform>(gameObject),
                AsyncWaitForComponents.WaitForComponentInParentAsync<Canvas>(gameObject)
            );

        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
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
   

   
}
