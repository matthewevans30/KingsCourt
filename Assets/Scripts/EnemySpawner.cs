using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    //create singleton, only need one spawner in game
    public static EnemySpawner Instance;

    int spawnIndex = 0;
    int totalSpawnPoints = 4;

    public GameObject enemy;
    public float spawnDelay = 2.5f;

    [SerializeField]
    Vector3 target;

    GameObject[] spawnPoints;
    List<Transform> spawnPositions = new List<Transform>();

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        //save location of each spawn point
        foreach(GameObject spawnPoint in spawnPoints)
        {
            spawnPositions.Add(spawnPoint.transform);
        }
    }

    //spawn enemies after delay amt of time
    public IEnumerator SpawnEnemies()
    {
        while (GameManager.gameIsActive)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }

        yield return null;
    }

    //spawn individual enemy at next spawn point 
    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemy, spawnPositions[spawnIndex%totalSpawnPoints]);

        newEnemy.GetComponent<NavMeshAgent>().SetDestination(target);
        spawnIndex++;
    }
}
