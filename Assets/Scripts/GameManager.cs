using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    [SerializeField] GameObject menuButton;
    [SerializeField] GameObject sensivilityMenuX;
    [SerializeField] GameObject sensivilityMenuY;
    [SerializeField] GameObject ImageMenu;
    public bool hasRecipeBook;
    public bool menuPressed;
    public int enemyCount;
    public bool readyForEnemies;
    public bool canAttack = true;
    public bool goForward;
    public bool speedBoost;

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
    }

    void Start()
    {
        EventManager.Instance.Register(GameEventTypes.OnRestart, Restart);
        hasRecipeBook = true;
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister(GameEventTypes.OnRestart, Restart);
    }

    private void Update()
    {
        if (onMinigame)
            canAttack = false;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuPressed = !menuPressed;
            menuButton.SetActive(menuPressed);
            sensivilityMenuX.SetActive(menuPressed);
            sensivilityMenuY.SetActive(menuPressed);
            ImageMenu.SetActive(menuPressed);   
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
