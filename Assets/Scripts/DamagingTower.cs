using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingTower : MonoBehaviour
{
    [SerializeField]
    private float delayBetweenShots = 2f;

    [SerializeField]
    private DamagingProjectile projectilePrefab;

    private readonly Queue<GameObject> targetQueue = new();
    private readonly HashSet<GameObject> targetsInRadius = new();
    private GameObject currentTarget = null;

    private Transform shootingPoint;

    private void Start()
    {
        shootingPoint = transform.Find("ShootingPoint");

        StartCoroutine(ShootCoroutine());
    }

    private void Update()
    {
        if (currentTarget == null && targetsInRadius.Contains(currentTarget))
        {
            targetsInRadius.Remove(currentTarget);
        }

        while (targetQueue.Count > 0 && !targetsInRadius.Contains(currentTarget))
        {
            currentTarget = targetQueue.Dequeue();
        }
    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            if (!currentTarget)
            {
                yield return new WaitForSeconds(delayBetweenShots);
            }

            Shoot();

            yield return new WaitForSeconds(delayBetweenShots);
        }
    }

    private void Shoot()
    {
        if (!currentTarget)
        {
            return;
        }

        DamagingProjectile newProjectile = Instantiate(
            projectilePrefab,
            shootingPoint.position,
            Quaternion.Euler(Constants.CameraXRotation, 0, 0)
        );

        newProjectile.Setup(currentTarget.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetQueue.Enqueue(other.gameObject);
            targetsInRadius.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (targetsInRadius.Contains(other.gameObject))
            {
                targetsInRadius.Remove(other.gameObject);
            }
        }
    }
}
