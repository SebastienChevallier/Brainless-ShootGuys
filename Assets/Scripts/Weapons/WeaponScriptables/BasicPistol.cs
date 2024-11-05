using UnityEngine;
[CreateAssetMenu(fileName = "BasicPistol", menuName = "Scriptable Objects/WeaponType/Pistol/basicPistol", order = 1)]

public class BasicPistol : Pistol
{
    public override void OnShoot()
    {
        base.OnShoot();
        Bullet bullet = Instantiate(bulletType);
        bullet.transform.position = originWeapon.transform.position;
        bullet.transform.rotation = originWeapon.transform.rotation;
        bullet.rb.linearVelocity = originWeapon.transform.forward * bulletSpeed;
    }
}
