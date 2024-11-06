using UnityEngine;
using BaseTemplate.Behaviours;
using UnityEngine.InputSystem;
using NUnit.Framework;
using System.Collections.Generic;

public class GameManager : MonoSingleton<GameManager>
{    
    public PlayerInputManager _PlayerInputManager;
    public List<PlayerInfo> _PlayerList;
    public int _NumberOfRounds;

    [Header("Player spawn Points")]
    [SerializeField]private List<Transform> spawnsTransform;
    [HideInInspector]public List<Transform> FreeSpawnsTransform;

    [Header("UI Panels")]
    public GameObject _WaitingPanel;

    public struct PlayerInfo
    {
        public PlayerInput playerInput;
        public int playerScore;

        public void Add(int score)
        {
            playerScore += score;
        }
    }

    private void Awake()
    {
        /*_ManagerInfo = ScriptableObject.CreateInstance<ManagerInfo>();
        _ManagerInfo.Init(); */
        
        FreeSpawnsTransform = spawnsTransform;
    }

    public void SpawnPlayer(PlayerInput playerInput)
    {
        if(FreeSpawnsTransform.Count <= 0)
        {
            FreeSpawnsTransform = spawnsTransform;
        }

        Transform position = FreeSpawnsTransform[Random.Range(0, FreeSpawnsTransform.Count)];
        FreeSpawnsTransform.Remove(position);

        playerInput.transform.position = position.position;
    }

    public void AddPlayer(PlayerInput player)
    {
        PlayerInfo playerTemp = new PlayerInfo();
        playerTemp.playerInput = player;
        playerTemp.playerScore = 0;
        _PlayerList.Add(playerTemp);        

        if(_PlayerList.Count > 1 ) 
        {
            _WaitingPanel.SetActive(false);
            StartGame();
        }
    }

    public void StartGame()
    {
        foreach(PlayerInfo player in _PlayerList )
        {
            if(player.playerInput.TryGetComponent<PlayerMovement>(out  var playerMovement))
            {
                SpawnPlayer(player.playerInput);
                playerMovement._CanMove = true;
            }
        }
    }

    public void EndGame()
    {

    }

    public void AddPoint(PlayerInput player)
    {
        foreach (PlayerInfo plr in _PlayerList)
        {
            if (plr.playerInput == player)
            {
                plr.Add(1);
            }
        }
    }
}
