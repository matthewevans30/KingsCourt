using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //only need one gameManager
    public static GameManager instance;

    int targetHealth = 100;
    int enemiesDestroyed;
    int gold = 60;
    int towerCost = 20;
    int level = 1;
    int enemiesPerLevel = 10;
    float spawnTimeIncrease = 0.89f;

    public static bool gameIsActive;
    public GameObject towerPrefab;
    public Transform towerSpawnTransform;
    public AudioSource GameAudio;

    void Start()
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

    //king is damaged, check if we lose and update stats
    public void TargetDamaged()
    {
        targetHealth -= 5;

        if (targetHealth <= 0 && gameIsActive)
        {
            LoseGame();
        }
        else
        {
            GameStatsManager.instance.UpdateStats
                (gold, targetHealth, level, towerCost);
            HapticController.Instance.SendHaptics();
        }
    }

    //startGame and spawn enemies after delay
    public void StartGame()
    {
        gameIsActive = true;
        GameAudio.Play();
        GameStatsManager.instance.UpdateStats(gold, targetHealth, level, towerCost);
        StartCoroutine(StartDelay());
    }

    //reset stats, remove enemies and towers
    public void RestartGame()
    {
        targetHealth = 100;
        gold = 60;
        level = 1;
        towerCost = 20;
        EnemySpawner.Instance.spawnDelay = 2.5f;

        TowerCreator.Instance.ResetVariables();

        GameObject[] towers = GameObject.FindGameObjectsWithTag("DefenseTower");
        foreach (GameObject tower in towers)
            Destroy(tower);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
            Destroy(enemy);

        GameStatsManager.instance.UpdateStats
            (gold, targetHealth, level, towerCost);

        GameAudio.Stop();
    }

    public void LoseGame()
    {
        gameIsActive = false;
        CanvasManager.instance.Lose(level, enemiesDestroyed);
    }

    //reward goal for enemy death, update level if necessary
    public void EnemyDestroyed()
    {
        enemiesDestroyed++;
        gold += 5;

        if(enemiesDestroyed >= level * enemiesPerLevel)
        {
            level++;
            EnemySpawner.Instance.spawnDelay *= spawnTimeIncrease;
        }
        GameStatsManager.instance.UpdateStats
            (gold, targetHealth, level, towerCost);
    }

    //check conditions for and build tower
    public void BuyTower()
    {
        if(gold < towerCost)
        {
            StartCoroutine(GameStatsManager.instance.InsufficientFundsWarning());
            return;
        }

        if (TowerCreator.Instance.BuildTower())
        {
            gold -= towerCost;
            towerCost += 20;
            GameStatsManager.instance.UpdateStats(gold, targetHealth, level, towerCost);
        }
        else
        {
            StartCoroutine(GameStatsManager.instance.MaxTowerWarning());
        }
    }

    //5 second delay before spawning enemies at start of game
    public IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(EnemySpawner.Instance.SpawnEnemies());
    }
}
