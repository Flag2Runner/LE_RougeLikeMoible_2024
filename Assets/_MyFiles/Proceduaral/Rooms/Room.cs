using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
class WallDoor
{
    [SerializeField] public GameObject wall;
    
    [SerializeField] public GameObject door;

    [SerializeField] public bool keepDoor;
}

public class Room : MonoBehaviour
{
    [SerializeField] private WallDoor[] _wallsDoors;
    [SerializeField] private string roomName = "Room";
    [SerializeField] private int height, width;
    [SerializeField] private Vector2 roomLocation = Vector2.zero;
    [SerializeField] private GameObject cameraTarget;

    public GameObject GetCameraTarget()
    {
        return cameraTarget;
    }

    public Vector2 GetRoomLocation()
    {
        return roomLocation;
    }

    public void UpdateRoom(bool[] keepDoor)
    {
        for (int i = 0; i < keepDoor.Length; i++)
        {
            _wallsDoors[i].door.SetActive(keepDoor[i]);
            _wallsDoors[i].wall.SetActive(!keepDoor[i]);
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
