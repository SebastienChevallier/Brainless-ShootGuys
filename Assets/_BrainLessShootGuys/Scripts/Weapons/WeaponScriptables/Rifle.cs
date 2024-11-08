using System.Collections;
using UnityEngine;

public class Rifle : WeaponType
{
    public Vector2 bulletSpeedMinMax;
    public float bulletSpeed;

    [Header("Rafale Only")]
    public bool isRafale;
    [Tooltip("Type 0 for automatic weapon")]
    public int bulletInRafale;
    public Vector2 timeToReloadRafaleMinMax;
    [Space(10)]

    protected float timeToReloadRafale;
    protected bool isShooting;
    protected bool isRafaling;
    public override void Init(Weapon weapon)
    {
        base.Init(weapon);
    }
    public override void OnShoot()
    {
        if (isShooting && isRafaling)
            return;

        base.OnShoot();

        isShooting = true;

        if (isRafale)
            originWeapon.StartCoroutine(ShootRafale());
        else if (!isRafale)
            originWeapon.StartCoroutine(ShootLoop());

    }

    public IEnumerator ShootLoop()
    {
        isRafaling = true;
        while (isShooting)
        {
            InstantiateBullet();
            yield return new WaitForSeconds(tireRate);
        }
        isRafaling = false;
    }
    
    public IEnumerator ShootRafale()
    {
        isRafaling = true;
        while (isShooting)
        {
            for (int i = 0; i < bulletInRafale; i++)
            {
                InstantiateBullet();
                yield return new WaitForSeconds(tireRate);
            }
            
            yield return new WaitForSeconds(timeToReloadRafale);
        }
        isRafaling = false;
    }
    public override void InstantiateBullet()
    {
        base.InstantiateBullet();
    }

    public override void StopShooting()
    {
        base.StopShooting();
        isShooting = false;
    }
    public override void DefineStats()
    {
        base.DefineStats();
        timeToReloadRafale = Random.Range(timeToReloadRafaleMinMax.x, timeToReloadRafaleMinMax.y);
        bulletSpeed = Random.Range(bulletSpeedMinMax.x, bulletSpeedMinMax.y);
    }
}
