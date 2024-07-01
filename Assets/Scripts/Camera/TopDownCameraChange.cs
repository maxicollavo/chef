using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TopDownCameraChange : MonoBehaviour
{
    [SerializeField] Transform topDownPos;
    [SerializeField] Transform fpsPos;

    [SerializeField] GameObject topDownCam;
    [SerializeField] GameObject fppCamera;

    public static bool changeCam;
    [SerializeField] private GameObject crosshair;
    private bool regularCamIsActive;
    [SerializeField] AudioSource sizzlingSource;

    private void Start()
    {
        EventManager.Instance.Register(GameEventTypes.ChangeCamera, ChangeCamera);
        regularCamIsActive = true;
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister(GameEventTypes.ChangeCamera, ChangeCamera);
    }

    private void ChangeCamera(object sender, EventArgs e)
    {
        ChangeCamera();
    }

    private void ChangeCamera()
    {
        if (changeCam)
        {
            fppCamera.SetActive(false);
            topDownCam.SetActive(true);
        }
        else if (!changeCam)
        {
            topDownCam.SetActive(false);
            fppCamera.SetActive(true);
        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("TopDown"))
    //    {
    //        regularCamIsActive = false;
    //        crosshair.SetActive(regularCamIsActive);
    //        changeCam = true;
    //        EventManager.Instance.Dispatch(GameEventTypes.ChangeCamera, this, EventArgs.Empty);
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TopDown"))
        {
            var cintas = other.GetComponent<Cintas>();
            if (cintas == null)
                sizzlingSource.Play();

            regularCamIsActive = false;
            crosshair.SetActive(regularCamIsActive);
            changeCam = true;
            EventManager.Instance.Dispatch(GameEventTypes.ChangeCamera, this, EventArgs.Empty);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TopDown"))
        {
            if (sizzlingSource.isPlaying)
                sizzlingSource.Stop();

            regularCamIsActive = true;
            crosshair.SetActive(regularCamIsActive);
            changeCam = false;
            EventManager.Instance.Dispatch(GameEventTypes.ChangeCamera, this, EventArgs.Empty);
        }
    }
}
