using System;
using System.Collections;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] AudioSource confDecisionSound;

    void Start()
    {
        EventManager.Instance.Register(GameEventTypes.OnConf, OnConfDecision);
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister(GameEventTypes.OnConf, OnConfDecision);
    }

    public void CantMove()
    {
        GameManager.Instance.onMinigame = true;
    }

    public void MoveAgain()
    {
        GameManager.Instance.onMinigame = false;
    }

    private void OnConfDecision(object sender, EventArgs e)
    {
        StartCoroutine(ConfDecisionCoroutine());
    }

    IEnumerator ConfDecisionCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        confDecisionSound.Play();
    }
}
