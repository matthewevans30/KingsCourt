using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class Tower : MonoBehaviour
{
    Transform target;
    public Vector3 spawnPosition;

    [Header("Attributes")]
    public float range = 0.5f;
    public float fireRate = 0.5f;
    public float fireCountdown = 0f;
    public Transform firePoint;

    public bool isOnBoard;
    public bool hoveringOverBoard;

    public GameObject bulletPrefab;
    public ActionBasedContinuousMoveProvider xrOrigin;

    // Start is called before the first frame update
    void Start()
    {
        //search for target every 0.2 seconds
        InvokeRepeating("UpdateTarget", 0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        //shoot if allowed, reset cooldown
        if(fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void UpdateTarget()
    {
        //tower must be placed to be actively finding targets
        if (isOnBoard)
        {
            //get list of enemies spawned, then find closest one
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;

            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            //check for valid target
            if (nearestEnemy != null && shortestDistance <= range)
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target = null;
            }
        }
    }

    //editor gizmo for range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    //create bullet, follow target
    void Shoot()
    {
        GameObject firedBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = firedBullet.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("LeftHand"))
        {
            xrOrigin.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
        }
    }

    //check where the tower is upon release
    //if its not on the board, return it to spawnPosition
    public void OnSelectExited(SelectExitEventArgs args)
    {
        StartCoroutine(CheckIfPlaced());  
    }

    private IEnumerator CheckIfPlaced()
    {
        yield return new WaitForSeconds(0.1f);

        if (!isOnBoard)
        {
            SendHome();
        }
    }

    public void SendHome()
    {
        transform.position = spawnPosition;
        isOnBoard = false;
        target = null;
    }
}
