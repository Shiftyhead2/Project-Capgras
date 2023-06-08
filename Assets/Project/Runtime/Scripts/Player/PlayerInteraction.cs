using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main class that is responsible for managing the way the player interacts with the game world
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    [Header("Camera")]
    [field:SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    private Camera cam;
    [Header("Interaction settings")]
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] private float Distance = 1f;
    private PlayerInputManager playerInputManager;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cam != null)
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            //Debug.DrawRay(ray.origin, ray.direction * Distance);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray,out hitInfo, Distance, LayerMask))
            {
                if(hitInfo.collider.TryGetComponent(out IInteractable interactable))
                {
                    if (playerInputManager.onFoot.Interact.triggered)
                    {
                        interactable.Interact();
                    }
                }
            }
        } 
    }
}
