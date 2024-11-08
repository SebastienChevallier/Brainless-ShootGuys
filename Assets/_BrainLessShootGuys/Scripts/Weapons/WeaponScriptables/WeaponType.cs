using UnityEngine;

public class WeaponType : ScriptableObject
{
    public Bullet bulletType;
    public int damage;
    public Vector2 tireRateMinMax;
    public Vector2 consumJaugeMinMax;

    public Weapon originWeapon;
    public float tireRate;
    public float consumJauge;

    public float jauge;

    public virtual void Init(Weapon weapon)
    {
        originWeapon = weapon;
        //A changer par le player
        bulletType.origin = weapon.gameObject;
        jauge = 100;
    }
    public virtual void OnShoot()
    {
        jauge -= consumJauge;
        if (jauge<=0 )
        {
            originWeapon.playerUse.UnEquip();
        }
    }
    public virtual void StopShooting()
    {

    }

    public virtual void DefineStats()
    {
        tireRate = Random.Range(tireRateMinMax.x, tireRateMinMax.y);
        consumJauge = Random.Range(consumJaugeMinMax.x, consumJaugeMinMax.y);
    }
}
