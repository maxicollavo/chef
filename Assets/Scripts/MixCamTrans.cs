using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MixCamTrans : MonoBehaviour
{
    public float speedTrans;
    public static MixCamTrans Instance; 
   public  IEnumerator Transition(Vector3 InitialPos,Vector3 InitialForward, Transform EndPos, Transform Cam)
    {
        float T = 0;
        while (T < 1) 
        {
            T += Time.deltaTime * speedTrans;

            Cam.position = Vector3.Lerp(InitialPos, EndPos.position, T);
            
            Cam.forward = Vector3.Lerp(InitialForward, EndPos.forward, T);

            yield return new WaitForEndOfFrame();
        }
   }

    public void TransitionActive(Transform EndPos, GameObject CameraOn, GameObject CameraOff)
    {
        Vector3 InitialPos = CameraOff.transform.position;
        Vector3 InitialForward = CameraOff.transform.forward;

        CameraOn.transform.position = InitialPos;
        CameraOn.transform.forward = InitialForward;
        CameraOn.SetActive(true);
        CameraOff.SetActive(false);
        StartCoroutine(Transition( InitialPos,  InitialForward,  EndPos, CameraOn.transform));
    }

    public void Awake()
    {
        Instance = this;
    }
}
