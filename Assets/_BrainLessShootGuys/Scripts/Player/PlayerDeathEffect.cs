using System.Collections;
using UnityEngine;

public class PlayerDeathEffect : MonoBehaviour
{
    public bool hasFinishedCoroutine;
    public float animationDuration;
    [Space(30)]

    public ParticleSystem deathEffect;
    public CameraShake ShakeComp;

    void Awake()
    {
        hasFinishedCoroutine = true;
    }

    public void DeathAnimation()
    {
        hasFinishedCoroutine = false;
        StartCoroutine(DeathAnimationCoroutine());
    }

    IEnumerator DeathAnimationCoroutine()
    {
        Instantiate(deathEffect, transform);
        ShakeComp.ShakeCamera();

        yield return new WaitForSeconds(animationDuration);

        hasFinishedCoroutine = true;
    }
}
