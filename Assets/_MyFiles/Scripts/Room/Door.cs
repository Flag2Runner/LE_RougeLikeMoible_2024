using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string doorDirection;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Player player = other.GetComponent<Player>();
            Debug.Log($"Moving the camera {doorDirection} door...");
            player.MoveCameraDirrection(doorDirection);
            Debug.Log("Camera finished moving!!!");
        }
    }
}
