using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The main class responsible for handling the mouse movement
/// </summary>
public class PlayerLook : MonoBehaviour
{

    [field:Header("Camera")]
    [field:SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    public Camera cam { get; private set; }

    private float xRotation = 0f;


    [Header("Mouse sensitivity settings")]
    [SerializeField]
    [Range(0f, 30f)]
    private float xSensitivity = 15f;
    [SerializeField]
    [Range(0f, 30f)]
    private float ySensitivity = 15f;

    [Header("Zoom settings")]
    [SerializeField] private float timeToZoom = 0.3f;
    [SerializeField] private float zoomF0V = 30f;
    private float defaultFOV;
    private Coroutine zoomRoutine;
    bool isZooming = false;

    private void OnEnable()
    {
        GameEvents.onHideMouse += DisableMouse;
        GameEvents.onShowMouse += EnableMouse;
    }

    private void OnDisable()
    {
        GameEvents.onHideMouse -= DisableMouse;
        GameEvents.onShowMouse -= EnableMouse;
    }


    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        defaultFOV = cam.fieldOfView;
        DisableMouse();
    }


    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }


    public void HandleZoom()
    {
      
        isZooming = !isZooming;
        

        if(zoomRoutine != null)
        {
            StopCoroutine(zoomRoutine);
            zoomRoutine = null;
        }

        zoomRoutine = StartCoroutine(ToggleZoom(isZooming));
    }


    private IEnumerator ToggleZoom(bool isEnter)
    {
        float targetFOV = isEnter ? zoomF0V : defaultFOV;
        float startingFOV = cam.fieldOfView;
        float timeElapsed = 0f;

        while(timeElapsed < timeToZoom)
        {
            cam.fieldOfView = Mathf.Lerp(startingFOV, targetFOV, timeElapsed / timeToZoom);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        cam.fieldOfView = targetFOV;
        zoomRoutine = null;
    }


    void DisableMouse()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void EnableMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
