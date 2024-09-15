using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo
{
    public string name;
    public Vector2 location = Vector2.zero;
}

public class RoomManager : MonoBehaviour
{
    [SerializeField] private string currrentWorldName = "The Basement";

    private Room currentLoadRoomData;
    
    private Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    private List<Room> loadedRooms = new List<Room>();

    private bool bIsLoadingRoom = false;

    public bool DoesRoomExist(Vector2 value)
    {
        return loadedRooms.Find(item => Mathf.Approximately(item.GetRoomLocation().x, value.x) && Mathf.Approximately(item.GetRoomLocation().y, value.y)) != null;
    }
}
