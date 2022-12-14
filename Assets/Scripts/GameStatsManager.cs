using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStatsManager : MonoBehaviour
{
    public TextMeshProUGUI goldText, healthText, levelText, costText, costWarning, towerWarning;

    public static GameStatsManager instance;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    //update all stats displayed on gameCanvas
    public void UpdateStats(int gold, int health, int level, int towerCost)
    {
        goldText.text = "Gold: " + gold;
        healthText.text = "Health: " + health;
        levelText.text = "Level: " + level;
        costText.text = "Cost: " + towerCost + " Gold";
    }

    //flash insufficient funds warning for two seconds
    public IEnumerator InsufficientFundsWarning()
    {
        costWarning.enabled = true;
        yield return new WaitForSeconds(2f);

        costWarning.enabled = false;
    }

    //flash max tower warning for two seconds
    public IEnumerator MaxTowerWarning()
    {
        towerWarning.enabled = true;
        yield return new WaitForSeconds(2f);

        towerWarning.enabled = false;
    }
}
