using System;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private string roomName = "Room";
    [SerializeField] private int height, width;
    [SerializeField] private Vector2 roomLocation = Vector2.zero;
    [SerializeField] private GameObject[] walls; //0->North 1->South 2->East 3->West
    [SerializeField] private GameObject[] doors;
    [SerializeField] private bool[] testStatus;

    public Vector2 GetRoomLocation()
    {
        return roomLocation;
    }

    private void Start()
    {
        UpdateRoom(testStatus);
    }

    void UpdateRoom(bool[] status)
    {
        for (int i = 0; i < status.Length; i++)
        {
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
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
