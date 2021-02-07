using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    PlayerControls controller;
    public string[] levels;
    public GameObject[] nonInGameUI;
    Scene scene;
    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
        controller = new PlayerControls();
        //attacks
        controller.Menu.Start.performed += ctx => StartButton();
        controller.Menu.Quit.performed += ctx => Quit();
        //controller.Gameplay.AtkLow.performed += ctx => ReturnToMainMenu();
    }
    private void OnEnable()
    {
        controller.Menu.Enable();
    }
    private void OnDisable()
    {
        controller.Menu.Disable();
    }
    public void StartButton()
    {
        if (scene.name == "MainMenu") {
            StartLevel();
        }
        else
        {
            Retry();
        }
    }
    public void StartLevel()
    {
        Debug.Log("startLevel");
        int levelNumber = Random.Range(0, levels.Length);
        SceneManager.LoadScene(levels[levelNumber]);
    }
    public void Quit()
    {
        if (scene.name == "MainMenu")
        {
            Application.Quit();
        }
    }
    public void Tutorial()
    {
        SceneManager.LoadScene("CaseTutorial");
    }
    public void ReturnToMainMenu()
    {
        bool open = false;
        foreach(GameObject ui in nonInGameUI)
        {
            if (ui.active == true)
            {
                open = true;
            }
        }
        switch (open)
        {
            case true:
                SceneManager.LoadScene("MainMenu");
                break;
            case false:
                break; 
        }
    }
    public void Retry()
    {        
        SceneManager.LoadScene(scene.name);
    }
}
