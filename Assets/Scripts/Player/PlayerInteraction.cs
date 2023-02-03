using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Camera")]
    [field:SerializeField, ReadOnlyInspector] private Camera cam;
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
            Debug.DrawRay(ray.origin, ray.direction * Distance);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray,out hitInfo, Distance, LayerMask))
            {
                IInteractable interactable = hitInfo.collider.GetComponent<IInteractable>();

                if(interactable != null)
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