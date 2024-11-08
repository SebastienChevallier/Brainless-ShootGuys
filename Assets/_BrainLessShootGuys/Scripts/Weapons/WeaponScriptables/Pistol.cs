using System.Collections;
using UnityEngine;

public class Pistol : WeaponType
{
    public Vector2 bulletSpeedMinMax;
    public float bulletSpeed;

    protected bool canShoot;
    public override void Init(Weapon weapon)
    {
        base.Init(weapon);
    }
    public override void OnShoot()
    {
        if (canShoot)
        {
            base.OnShoot();
            originWeapon.StartCoroutine(WaitingForShoot());
        }
    }

    public virtual IEnumerator WaitingForShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(tireRate);
        canShoot = true;
    }

    public override void DefineStats()
    {
        base.DefineStats();
        bulletSpeed = Random.Range(bulletSpeedMinMax.x, bulletSpeedMinMax.y);
    }
}
