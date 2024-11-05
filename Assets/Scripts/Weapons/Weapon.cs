using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    public WeaponVisual weaponVisual;
    public ActifSpell actifSpell;
    public WeaponType weaponType;
    public Transform weaponVisualParent;
    #region test

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) {
            Shoot();
        }
    }
    #endregion

    public virtual void Init()
    {
        //actifSpell.Init(this);
        weaponType.Init(this);
    }

    public void Shoot() {
        weaponType.OnShoot();
        weaponVisual.OnShoot();
    }

    public void UseActifSpell()
    {
        actifSpell.OnUse();
    }
}
