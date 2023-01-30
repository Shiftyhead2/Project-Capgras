using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask interactiableLayerMask;
    [SerializeField] private float interactionDistance = 1f;
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
            Debug.DrawRay(ray.origin, ray.direction * interactionDistance);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray,out hitInfo, interactionDistance, interactiableLayerMask))
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
