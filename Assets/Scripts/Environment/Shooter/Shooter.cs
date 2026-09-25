using System;
using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] float fireRate = 1f;
    [SerializeField] float secondsBeforeStart = 1f;
    [SerializeField] float maxDistance = 10f;
    [SerializeField] Vector3 firePosition;
    [SerializeField] Vector2 fireDirection = Vector2.right;
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] float projectileSpeed;
    [SerializeField] int poolSize = 10;
    private Vector3 poolPos = new Vector3(-1000, -1000, 0);
    Projectile[] projectilePool;

    private void Awake()
    {
        projectilePool = new Projectile[poolSize];
        for(int i = 0;  i < poolSize; i++)
        {
            Projectile projectile = Instantiate(projectilePrefab, poolPos, Quaternion.identity, gameObject.transform);
            projectile.gameObject.SetActive(false);
            projectile.SetSpeed(projectileSpeed);
            projectilePool[i] = projectile;
        }
    }
    private void Start()
    {
        StartCoroutine(_StartShootingCoroutine());
    }

    private IEnumerator _StartShootingCoroutine()
    {
        yield return new WaitForSeconds(secondsBeforeStart);
        while (true)
        {
            MakeShot();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void MakeShot()
    {
        Projectile nextProjectile = GetNextProjectile();
        if (nextProjectile == null) return;
        nextProjectile.transform.position = transform.position + firePosition;
        nextProjectile.gameObject.SetActive(true);
        nextProjectile.Launch(fireDirection, maxDistance);
    }

    private Projectile GetNextProjectile()
    {
        foreach(Projectile projectile in projectilePool)
        {
            if(projectile.gameObject.activeSelf == false) return projectile;  
        }
        return null;
    }
}
