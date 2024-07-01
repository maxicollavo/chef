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

    float xRotation;
    float yRotation;
    public Slider SliderX;
    public Slider SliderY;

    private bool isMouseActive = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        QualitySettings.vSyncCount = 1;
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.onMinigame)
        {
            sensX = SliderX.value;
            sensY = SliderY.value;
            if (TopDownCameraChange.changeCam || !isMouseActive) { return; }

            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, minClamp, maxClamp);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }

    private void ToggleMouseMovement()
    {
        isMouseActive = !isMouseActive;
        Cursor.lockState = isMouseActive ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isMouseActive;
    }
}