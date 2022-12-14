using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTower : MonoBehaviour
{

    //report enemy collision, destroy enemy
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.TargetDamaged();
            Destroy(other.transform.parent.gameObject);
        }
    }
}
