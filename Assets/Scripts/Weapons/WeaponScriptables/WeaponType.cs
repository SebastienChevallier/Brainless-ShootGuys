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

    public virtual void Init(Weapon weapon)
    {
        originWeapon = weapon;
        //A changer par le player
        bulletType.origin = weapon.gameObject;
    }
    public virtual void OnShoot()
    {
        
    }

    public virtual void DefineStats()
    {
        tireRate = Random.Range(tireRateMinMax.x, tireRateMinMax.y);
        consumJauge = Random.Range(consumJaugeMinMax.x, consumJaugeMinMax.y);
    }
}
