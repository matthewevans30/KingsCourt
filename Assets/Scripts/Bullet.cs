using UnityEngine;

public class Bullet : MonoBehaviour
{
    //pawn we are seeking
    private Transform target;

    public float speed;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        //no target, no reason for existing
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        //calculate direction and distance we will move
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        //if distance between us and enemy is less than amount we will move,
        //we have a hit
        if(dir.magnitude <= distanceThisFrame)
        {
            HitEnemy();
            return;
        }

        //else we move distancethisframe in direction of enemy
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    //enemy hit, destroy both and log kill
    void HitEnemy()
    {
        Destroy(gameObject);
        Destroy(target.parent.gameObject);
        GameManager.instance.EnemyDestroyed();
    }
}
