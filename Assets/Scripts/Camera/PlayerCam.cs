using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public float minClamp;
    public float maxClamp;

    public Transform orientation;

    private float xRotation;
    private float yRotation;
    private float targetXRotation;
    private float targetYRotation;

    public Slider SliderX;
    public Slider SliderY;

    private bool isMouseActive = true;

    public float lerpSpeed = 10f; // Interpolation speed for camera rotation

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        QualitySettings.vSyncCount = 1;
    }

    private void LateUpdate()
    {
        sensX = SliderX.value;
        sensY = SliderY.value;

        if (TopDownCameraChange.changeCam || !isMouseActive)
        {
            return;
        }

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        targetYRotation += mouseX;
        targetXRotation -= mouseY;
        targetXRotation = Mathf.Clamp(targetXRotation, minClamp, maxClamp);

        xRotation = Mathf.Lerp(xRotation, targetXRotation, Time.deltaTime * lerpSpeed);
        yRotation = Mathf.Lerp(yRotation, targetYRotation, Time.deltaTime * lerpSpeed);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void ToggleMouseMovement()
    {
        isMouseActive = !isMouseActive;
        Cursor.lockState = isMouseActive ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isMouseActive;
    }
}