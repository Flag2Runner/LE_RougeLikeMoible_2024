using System;
using UnityEngine;
[RequireComponent(typeof(RoomManager))]
public class GameManager : MonoBehaviour
{
    private RoomManager _roomManager;
    private void Awake()
    {
        _roomManager = GetComponent<RoomManager>();
    }
}
