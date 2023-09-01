using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public static class AsyncWaitForComponents
{
    public static async UniTask WaitForFindComponentAsync<T>() where T: Component
    {
        await UniTask.WaitUntil(() => Object.FindObjectOfType<T>() != null);
    }

    public static async UniTask WaitForComponentAsync<T>(GameObject gameObject) where T : Component
    {
        await UniTask.WaitUntil(() => gameObject.GetComponent<T>() != null);
    }

    public static async UniTask WaitForComponentInChildrenAsync<T>(GameObject parent) where T: Component
    {
        await UniTask.WaitUntil(() => parent.GetComponentInChildren<T>() != null);
    }

    public static async UniTask WaitForComponentInParentAsync<T>(GameObject child) where T : Component
    {
        await UniTask.WaitUntil(() => child.GetComponentInParent<T>() != null);
    }

    public static async UniTask WaitForCamAsync<T>(GameObject gameObject) where T: PlayerLook
    {
        await UniTask.WaitUntil(() => gameObject.GetComponent<T>().cam != null);
    }
}
