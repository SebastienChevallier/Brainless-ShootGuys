using UnityEngine;
public class WeaponVisual : MonoBehaviour
{
    ParticleSystem shootParticles;
    public void OnShoot()
    {
        shootParticles.Play();
    }
}
