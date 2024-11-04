using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [HideInInspector] public float _CurrentHealth;
    public float _MaxHealth;

    [HideInInspector] public float _Speed;
    public float _BaseSpeed;

    public void Init()
    {
        _CurrentHealth = _MaxHealth;
        _Speed = _BaseSpeed;
    }
}
