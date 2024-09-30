using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string doorDirection;
    [SerializeField] private bool bisColliderEnabled;
    
    private Collider _doorCollider;

    private void Awake()
    {
        _doorCollider = GetComponent<Collider>();
        bisColliderEnabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player(Clone)" && bisColliderEnabled)
        {
                _doorCollider.enabled.Equals(false);
                Player player = other.GetComponent<Player>();
                Debug.Log($"Moving the camera {doorDirection} door...");
                player.MoveCameraDirrection(doorDirection);
                Debug.Log("Camera finished moving!!!");
        }
    }

    public void SetIsColliderEnabled(bool isPlayerInRoom)
    {
        bisColliderEnabled = isPlayerInRoom;
    }
}
