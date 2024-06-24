using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTracker : MonoBehaviour
{
    [SerializeField] private GameObject lastTutorialLimit;

    [SerializeField] private GameObject panelCanvas;
    [SerializeField] private GameObject movementTutorialText;
    [SerializeField] private GameObject killEnemyTutorialText;
    [SerializeField] private GameObject sendEnemyTutorialText;

    private bool WPressed, APressed, SPressed, DPressed, CPressed, SpacePressed, allPressed;
    private bool recipeChecked, enemySend;

    private void Start()
    {
        movementTutorialText.SetActive(true);
        EventManager.Instance.Register(GameEventTypes.OnAbility, OnAbility);
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister(GameEventTypes.OnAbility, OnAbility);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            APressed = true;
        if (Input.GetKeyDown(KeyCode.S))
            SPressed = true;
        if (Input.GetKeyDown(KeyCode.D))
            DPressed = true;
        if (Input.GetKeyDown(KeyCode.W))
            WPressed = true;
        if (Input.GetKeyDown(KeyCode.C))
            CPressed = true;
        if (Input.GetKeyDown(KeyCode.Space))
            SpacePressed = true;
        if (WPressed && APressed && SPressed && DPressed && CPressed && SpacePressed && !allPressed)
        {
            allPressed = true;
            movementTutorialText.SetActive(false);
        }

        if (GameManager.Instance.enemyCount == 1)
        {
            killEnemyTutorialText.SetActive(false);
            sendEnemyTutorialText.SetActive(true);
        }

        if (enemySend && GameManager.Instance.enemyCount == 2)
        {
            sendEnemyTutorialText.SetActive(false);
            panelCanvas.SetActive(false);
        }
        if (GameManager.Instance.enemyCount == 10)
            lastTutorialLimit.SetActive(false);
    }

    private void OnAbility(object sender, EventArgs e)
    {
        enemySend = true;
    }
}