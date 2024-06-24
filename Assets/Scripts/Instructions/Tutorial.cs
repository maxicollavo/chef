using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject tutorialText;
    [SerializeField] GameObject lastTutorialText;
    [SerializeField] bool lastText;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("PlayerCollider"))
        {
            if (lastText)
            {
                StartCoroutine(LastTextCoroutine());
            }
            else
            {
                lastTutorialText.SetActive(false);
                tutorialText.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator LastTextCoroutine()
    {
        lastTutorialText.SetActive(false);
        tutorialText.SetActive(true);
        yield return new WaitForSeconds(3f);
        tutorialText.SetActive(false);
        panel.SetActive(false);
        gameObject.SetActive(false);
        lastText = false;
    }
}
