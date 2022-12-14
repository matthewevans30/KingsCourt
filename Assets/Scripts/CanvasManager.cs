using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//this script handles menu button navigations
public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    [SerializeField]
    GameObject mainMenu, instructionMenu, gameMenu, loseMenu;

    [SerializeField]
    public TextMeshProUGUI endStats;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartButton()
    {
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        GameManager.instance.StartGame();
    }

    public void InstructionButton()
    {
        mainMenu.SetActive(false);
        instructionMenu.SetActive(true);
    }

    public void MenuButton()
    {
        instructionMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void RestartGame()
    {
        loseMenu.SetActive(false);
        mainMenu.SetActive(true);
        GameManager.instance.RestartGame();
        
    }

    public void BuyTowerButton()
    {
        GameManager.instance.BuyTower();
    }

    public void Lose(int level, int enemiesDefeated)
    {
        gameMenu.SetActive(false);
        loseMenu.SetActive(true);
        endStats.text = "The king has been defeated.\r\n\r\nLevels Survived: " 
            + level + "\r\nTotal Enemies Taken: " + enemiesDefeated;
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    
}
