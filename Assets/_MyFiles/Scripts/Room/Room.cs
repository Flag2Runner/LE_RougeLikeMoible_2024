using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class WallDoor
{
    [SerializeField] public GameObject wall;
    
    [SerializeField] public GameObject door;

    [SerializeField] public GameObject collider;
}

public class Room : MonoBehaviour
{
    [SerializeField] private WallDoor[] wallsDoors;
    

    public WallDoor[] WallsDoors => wallsDoors;
    [SerializeField] private string roomName = "Room";
    [SerializeField] private Vector2 roomLocation = Vector2.zero;
    [SerializeField] private bool bIsPlayerInRoom;
    [SerializeField] private float collisionWaitTime = 0.5f;
    private void Start()
    {
        roomName = gameObject.name;
    }
    public Vector2 GetRoomLocation()
    {
        return roomLocation;
    }

    public void InitilizeRoom(bool[] keepDoor)
    {
        
        for (int i = 0; i < wallsDoors.Length; i++)
        {
            WallsDoors[i].door.SetActive(keepDoor[i]);
            WallsDoors[i].wall.SetActive(!keepDoor[i]);
            WallsDoors[i].collider.SetActive(!keepDoor[i]);
        }

    }
    public void UpdateCollision()
    {
            for (int i = 0; i < wallsDoors.Length; i++)
            {
                WallsDoors[i].collider.SetActive(bIsPlayerInRoom);
            }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player(Clone)")
        {
            bIsPlayerInRoom = true;
            Debug.Log("WaitingOnPlayer");
            StartCoroutine(WaitOnPlayer());
            Debug.Log("Entered Room Trigger!!!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player(Clone)")
        {
            bIsPlayerInRoom = false;
            Debug.Log("WaitingOnPlayer");
            StartCoroutine(WaitOnPlayer());
            Debug.Log("Exited Room Trigger!!!");
        }

    }

    IEnumerator WaitOnPlayer()
    {
        yield return new WaitForSeconds(collisionWaitTime);
        UpdateCollision();
    }
}
