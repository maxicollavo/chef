using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    [SerializeField] GameObject pauseMenu;
    public bool hasRecipeBook;
    public bool menuPressed;
    public int enemyCount;
    public bool readyForEnemies;
    public bool canAttack = true;
    public bool goForward;
    public bool speedBoost;
    public bool onBoss;
    public bool goodChef;
    public bool bossAlive;
    public bool hasLever;
    public bool leverDone;

    public bool enemyAttacked;
    public bool onMinigame;

    public bool readyToInstantiate;
    public float ingredientCount;
    public bool ingredientReady;
    [SerializeField] GameObject ingredientLimit;
    [SerializeField] TextMeshProUGUI ingredientUGUI;
    public List<GameObject> enemies = new List<GameObject>();

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1;
    }

    void Start()
    {
        EventManager.Instance.Register(GameEventTypes.OnRestart, Restart);
        hasRecipeBook = true;
        bossAlive = true;
        ingredientCount = 0;
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister(GameEventTypes.OnRestart, Restart);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canAttack = !canAttack;
            menuPressed = !menuPressed;
            pauseMenu.SetActive(menuPressed);
            if (menuPressed)
            {
                Time.timeScale = 0; 
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1; 
            }
        }

        if (ingredientReady && ingredientCount >= 10)
        {
            readyForEnemies = false;
            ingredientLimit.SetActive(false);
        }

        ingredientUGUI.text = "Ingredients: " + ingredientCount.ToString();
    }

    private void Restart(object sender, EventArgs e)
    {
        SceneManager.LoadScene("LoseMenu");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
