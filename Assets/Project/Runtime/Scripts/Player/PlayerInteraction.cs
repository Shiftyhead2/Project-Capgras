using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// Main class that is responsible for managing the way the player interacts with the game world
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    [Header("Camera")]
    [field: SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    private Camera cam;
    [Header("Interaction settings")]
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] private float Distance = 1f;
    private PlayerInputManager playerInputManager;

    private const string INTERACT_ACTION = "Interact";


    async void Awake()
    {
        await IntilializeAsync();
    }

    private async UniTask IntilializeAsync()
    {
        await UniTask.Yield(PlayerLoopTiming.Initialization);
        await UniTask.WhenAll(AsyncWaitForComponents.WaitForComponentAsync<PlayerLook>(gameObject), 
            AsyncWaitForComponents.WaitForComponentAsync<PlayerInputManager>(gameObject), 
            AsyncWaitForComponents.WaitForCamAsync<PlayerLook>(gameObject));

        cam = GetComponent<PlayerLook>().cam;
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cam != null)
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            //Debug.DrawRay(ray.origin, ray.direction * Distance);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, Distance, LayerMask))
            {
                if (hitInfo.collider.TryGetComponent(out IInteractable interactable))
                {
                    if (playerInputManager.playerInput.actions[INTERACT_ACTION].IsPressed())
                    {
                        interactable.Interact();
                    }
                }
            }
        }
    }
}
