using System;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(RoomManager))]
public class GameManager : MonoBehaviour
{
    private RoomManager _roomManager;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerSpawnTarget;

    private GameObject _player;

    public RoomManager GetRoomManager()
    {
        return _roomManager;
    }

    private void Awake()
    {
        _roomManager = GetComponent<RoomManager>();
        _roomManager.StartAlgorithim();
        CreatePlayer();
        
    }

    private void CreatePlayer()
    {
        if (!_player&& !player) { return; }
        _player = Instantiate(player, transform, true);
        _player.transform.position = playerSpawnTarget.transform.position;
        
    }
}
