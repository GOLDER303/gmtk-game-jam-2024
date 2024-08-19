using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingProjectile : Projectile
{
    [SerializeField]
    private float movementSpeed = 1f;

    private Transform targetEnemy;
    private Vector3 targetEnemyPosition;

    public void Setup(Transform targetEnemy)
    {
        this.targetEnemy = targetEnemy;
        targetEnemyPosition = targetEnemy.position;
    }

    void Update()
    {
        if (targetEnemy)
        {
            targetEnemyPosition = targetEnemy.position;
        }
        else if (Vector3.Distance(transform.position, targetEnemyPosition) < .5f)
        {
            Destroy(gameObject);
        }

        Vector2 enemyPositionOnXYPlane = new(targetEnemyPosition.x, targetEnemyPosition.y);
        Vector2 projectilePositionOnXYPlane = new(transform.position.x, transform.position.y);

        float newRotation = Vector2.SignedAngle(
            Vector2.up,
            (enemyPositionOnXYPlane - projectilePositionOnXYPlane).normalized
        );

        transform.SetPositionAndRotation(
            Vector3.MoveTowards(
                transform.position,
                targetEnemyPosition,
                movementSpeed * Time.deltaTime
            ),
            Quaternion.Euler(0f, 0f, newRotation)
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
