using System;
using UnityEngine;

[Serializable]
public class WallDoor
{
    [SerializeField] public GameObject wall;
    
    [SerializeField] public GameObject door;
}

public class Room : MonoBehaviour
{
    [SerializeField] private WallDoor[] wallsDoors;
    private WallDoor[] _wallDoors = new WallDoor[4];
    

    public WallDoor[] WallsDoors => wallsDoors;
    [SerializeField] private string roomName = "Room";
    [SerializeField] private int height, width;
    [SerializeField] private Vector2 roomLocation = Vector2.zero;

    public Vector2 GetRoomLocation()
    {
        return roomLocation;
    }

    public void UpdateRoom(bool[] keepDoor)
    {
        
        for (int i = 0; i < wallsDoors.Length; i++)
        {
            wallsDoors[i].door.SetActive(keepDoor[i]);
            wallsDoors[i].wall.SetActive(!keepDoor[i]);
        }

    }

    public Vector3 GetRoomCenter()
    {
        return new Vector2(roomLocation.x * width, roomLocation.y * height);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color =Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, 2, height));
    }
}
