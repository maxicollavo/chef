using System;
using UnityEngine;

public class MixSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource rightSound;
    [SerializeField] AudioSource wrongSound;

    void Awake()
    {
        EventManager.Instance.Register(GameEventTypes.OnRightChoice, RightChoice);
        EventManager.Instance.Register(GameEventTypes.OnWrongChoice, WrongChoice);
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister(GameEventTypes.OnRightChoice, RightChoice);
        EventManager.Instance.Unregister(GameEventTypes.OnWrongChoice, WrongChoice);
    }

    private void WrongChoice(object sender, EventArgs e)
    {
        wrongSound.Play();
    }

    private void RightChoice(object sender, EventArgs e)
    {
        rightSound.Play();
    }
}
