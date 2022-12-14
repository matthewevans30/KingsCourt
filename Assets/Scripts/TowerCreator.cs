using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCreator : MonoBehaviour
{
    //only need one towercreator in game
    public static TowerCreator Instance;

    public GameObject towerPrefab;
    public Transform startPosition;
    public Vector3 originalStartLocation;
    int numTowersCreated;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        originalStartLocation = startPosition.position;
    }

    public bool BuildTower()
    {
        //limit amt of towers to increase difficulty/strategizing
        if (numTowersCreated > 10)
        {
            return false;
        }
        else
        {
            //create tower and save its start position in its tower script
            GameObject tower = Instantiate(towerPrefab, startPosition.position, startPosition.rotation);
            Tower towerScript = tower.GetComponent<Tower>();

            if (towerScript != null)
            {
                towerScript.spawnPosition = startPosition.position;
            }

            //move tower spawn position to get ready for next one
            startPosition.position = new Vector3(startPosition.position.x + 0.3f, startPosition.position.y, startPosition.position.z);
            numTowersCreated++;
        }
        return true;
    }

    public void ResetVariables()
    {
        numTowersCreated = 0;
        startPosition.position = originalStartLocation;
    }
}
