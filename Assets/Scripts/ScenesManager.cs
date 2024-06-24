using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene("ChefKitchen");
    }
    public void LoadMiniGame()
    {
        SceneManager.LoadScene("MixMiniGames");
    }
    public void LoadControls()
    {
        SceneManager.LoadScene("ControlMenu");
    }
    public void ExitMenu()
    {
        Application.Quit();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
