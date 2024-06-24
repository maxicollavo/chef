using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseIngredient : MonoBehaviour
{
    [SerializeField] Camera mixCam;
    private bool onAction;
    private WaitForSeconds waitTime = new WaitForSeconds(2f);

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mixCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && !onAction)
            {
                CheckAndAnimate<TomatoBowl>(hit.collider, "Se anima tomate");
                CheckAndAnimate<CarrotBowl>(hit.collider, "Se anima zanahoria");
                CheckAndAnimate<CucumberBowl>(hit.collider, "Se anima pepino");
                CheckAndAnimate<LettuceBowl>(hit.collider, "Se anima lechuga");
                CheckAndAnimate<OliveOil>(hit.collider, "Se anima aceite");
                CheckAndAnimate<Salt>(hit.collider, "Se anima sal");
            }
        }
    }

    private IEnumerator StopAnimCoroutine()
    {
        onAction = true;
        yield return waitTime;
        onAction = false;
    }

    void CheckAndAnimate<T>(Collider collider, string message) where T : Component
    {
        T component = collider.GetComponent<T>();
        if (component != null)
        {
            Animator anim = component.transform.parent.GetComponent<Animator>();
            Debug.Log(anim);
            if (anim != null)
            {
                anim.SetTrigger("OnAction");
                StartCoroutine(StopAnimCoroutine());
            }
        }
    }
}
