using UnityEngine;
using UnityEngine.UIElements;

public class UIMoveRect : MonoBehaviour
{
    public PlayerMovement player;
    public Vector3[] positions;

    void Start()
    {
        PlayerMovement[] players = FindObjectsOfType<PlayerMovement>();
        for (int i = 0; i < players.Length; i++)
        {
            transform.localPosition = positions[i];
        }
    }
}
