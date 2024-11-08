using UnityEngine;
[CreateAssetMenu(fileName = "BasicPistol", menuName = "Scriptable Objects/WeaponType/Pistol/basicPistol", order = 1)]

public class BasicPistol : Pistol
{
    public float bulletScale;
    public override void OnShoot()
    {
        base.OnShoot();
        Bullet bullet = Instantiate(bulletType);
        bullet.weaponType = this;
        bullet.origin = originWeapon.playerUse.gameObject;
        bullet.transform.position = originWeapon.playerUse._bulletSpawnTransform.position;
        bullet.transform.rotation = originWeapon.playerUse._bulletSpawnTransform.rotation;
        bullet.transform.localScale = Vector3.one * bulletScale;
        bullet.rb.linearVelocity = originWeapon.playerUse._bulletSpawnTransform.forward * bulletSpeed;
    }
}
