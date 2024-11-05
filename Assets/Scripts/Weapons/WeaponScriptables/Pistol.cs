using UnityEngine;

public class Pistol : WeaponType
{
    public Vector2 bulletSpeedMinMax;
    public float bulletSpeed;
    public override void Init(Weapon weapon)
    {
        base.Init(weapon);
    }
    public override void OnShoot()
    {
        base.OnShoot();
    }

    public override void DefineStats()
    {
        base.DefineStats();
        bulletSpeed = Random.Range(bulletSpeedMinMax.x, bulletSpeedMinMax.y);
    }
}
