using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class WallDoor
{
    [SerializeField] public GameObject wall;
    
    [SerializeField] public GameObject door;
}

public class Room : MonoBehaviour
{
    [SerializeField] private WallDoor[] wallsDoors;
    

    public WallDoor[] WallsDoors => wallsDoors;
    [SerializeField] private string roomName = "Room";
    [SerializeField] private int height, width;
    [SerializeField] private Vector2 roomLocation = Vector2.zero;
    [SerializeField] private bool bIsPlayerInRoom;

    public Vector2 GetRoomLocation()
    {
        return roomLocation;
    }

    public void UpdateRoom(bool[] keepDoor)
    {
        
        for (int i = 0; i < wallsDoors.Length; i++)
        {
            WallsDoors[i].door.SetActive(keepDoor[i]);
            WallsDoors[i].wall.SetActive(!keepDoor[i]);
            WallsDoors[i].door.GetComponent<Door>().SetIsColliderEnabled(keepDoor[i]);
        }

    }
    public void UpdateCollision()
    {
            for (int i = 0; i < wallsDoors.Length; i++)
            {
                WallsDoors[i].door.GetComponent<Door>().SetIsColliderEnabled(bIsPlayerInRoom);
            }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player(Clone)")
        {
            bIsPlayerInRoom = true;
            StartCoroutine(WaitOnPlayer());
            UpdateCollision();
            Debug.Log("Entered Room Trigger!!!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player(Clone)")
        {
            bIsPlayerInRoom = false;
            StartCoroutine(WaitOnPlayer());
            UpdateCollision();
            Debug.Log("Exited Room Trigger!!!");
        }

    }

    IEnumerator WaitOnPlayer()
    {
        yield return new WaitForSeconds(1.5f);
    }
}
