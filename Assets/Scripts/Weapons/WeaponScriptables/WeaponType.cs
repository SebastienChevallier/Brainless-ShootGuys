using UnityEngine;

public class WeaponType : ScriptableObject
{
    public Bullet bulletType;
    public int damage;
    public Vector2 tireRateminMax;
    public Vector2 consumJaugeMinMax;

    public Weapon originWeapon;
    public virtual void Init(Weapon weapon)
    {
        originWeapon = weapon;
    }
    public virtual void OnShoot()
    {
            
    }
}
