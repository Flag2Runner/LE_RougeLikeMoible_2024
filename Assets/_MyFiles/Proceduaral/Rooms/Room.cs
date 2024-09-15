using System;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private string roomName = "Room";
    [SerializeField] private int height, width, length;
    [SerializeField] private Vector2 roomLocation = Vector2.zero;

    public Vector2 GetRoomLocation()
    {
        return roomLocation;
    }

    public Vector3 GetRoomCenter()
    {
        return new Vector3(roomLocation.x * width, roomLocation.y * height);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color =Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, length));
    }
}
