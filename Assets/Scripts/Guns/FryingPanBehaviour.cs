using UnityEngine;

public class FryingPanBehaviour : MonoBehaviour
{
    [SerializeField] Animator FryingAnimation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Defence();
        }

        else if (Input.GetKeyUp(KeyCode.E))
        {
            StopDefence();
        }
    }

    private void Defence()
    {
        FryingAnimation.SetTrigger("OnAction");
    }

    private void StopDefence()
    {
        FryingAnimation.SetTrigger("OnStop");
    }
}
