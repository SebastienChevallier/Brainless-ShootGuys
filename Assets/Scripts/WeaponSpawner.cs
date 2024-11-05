using System.Collections;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public float spawnWeaponTime;
    public Vector2 firstSpawnDelayMinMax;
    public Transform weaponVisualTransformParent;

    public Weapon tempWeaponSpawn;

    private float firstSpawnDelay;
    private void Start()
    {
        LevelManager.Instance.weaponSpawners.Add(this);
        firstSpawnDelay = Random.Range(firstSpawnDelayMinMax.x, firstSpawnDelayMinMax.y);
    }

    public void Init()
    {
        StartCoroutine(SpawnWeapon(true));
    }

    IEnumerator SpawnWeapon(bool isFirstTime)
    {
        if (isFirstTime)
            yield return new WaitForSeconds(firstSpawnDelay);
        else 
            yield return new WaitForSeconds(spawnWeaponTime);

        tempWeaponSpawn = Instantiate(LevelManager.Instance.weaponInMap[Random.Range(0, LevelManager.Instance.weaponInMap.Count)], weaponVisualTransformParent);
        tempWeaponSpawn.enabled = false;
        //tempWeaponVisual = Instantiate(tempWeaponSpawn.weaponVisual, weaponVisualTransformParent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (other.TryGetComponent<PlayerMovement>(out PlayerMovement pm))
                pm.Equip(tempWeaponSpawn);
            //If (playerWeaon == null)
            //   PlayerWeapon = weapon;
            StartCoroutine(SpawnWeapon(false));
        }
    }
}
