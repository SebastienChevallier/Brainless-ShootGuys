using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicRifle", menuName = "Scriptable Objects/WeaponType/Rifle/basicRifle", order = 1)]
public class BasicRifle : Rifle
{
    public float bulletScale;
    public override void InstantiateBullet()
    {
        base.InstantiateBullet();
        Bullet bullet = Instantiate(bulletType);
        bullet.weaponType = this;
        bullet.origin = originWeapon.playerUse.gameObject;
        bullet.transform.position = originWeapon.playerUse._bulletSpawnTransform.position;
        bullet.transform.rotation = originWeapon.playerUse._bulletSpawnTransform.rotation;
        bullet.transform.localScale = Vector3.one * bulletScale;
        bullet.rb.linearVelocity = originWeapon.playerUse._bulletSpawnTransform.forward * bulletSpeed;
    }
}