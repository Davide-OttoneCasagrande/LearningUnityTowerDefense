using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Attributes")]
    public float fireRate = 10f;
    public float range = 30f;
    public float turnSpeed = 10f;

    [Header("Unity Config Fields")]
    public string EnemyTag = "Enemy";
    public Transform partToRotate;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public ParticleSystem muzzleFlash;

    Transform target = null;
    float shootContdown = 0f;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
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
        if (nearestEnemy != null && shortestDistance <= range)
            target = nearestEnemy.transform;
        else
            target = null;
    }

    void Update()
    {
        if (target == null)
            return;
        //Target Lock on
        Vector3 direction = target.position - transform.position;
        Quaternion LookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, LookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        //shoot
        if (shootContdown <= 0)
        {
            Shoot();
            shootContdown = 1f / fireRate;
        }
        shootContdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        muzzleFlash.Play();

        if (bullet.TryGetComponent<Bullet>(out var shooting))
        {
            shooting.Seek(target);
        }
    }

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(target.position, range);
    //}
}