using System;
using System.Diagnostics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
[ExecuteAlways]

public class ViewCamera : MonoBehaviour
{

    [SerializeField] private Transform pitchTransform;
    [SerializeField] private Camera viewCamera;
    [SerializeField] private float armLength = 12f;
    [SerializeField] private float cameraTurnSpeed = 30f;
    [SerializeField] private float CameraMoveLerpScale = 5f;

    private Transform _parentTransform;

    public Camera GetViewCamera()
    {
        return viewCamera;
    }

    public void MoveCamera(string doorDirection, Vector2 directionValue)
    {
        switch (doorDirection)
        {
            case "North":
                directionValue.x = transform.position.x;
                transform.Translate(LerpCamera(transform.position.z ,directionValue, CameraMoveLerpScale, true));
                break;
            case "South":
                directionValue.x = transform.position.x;
                transform.Translate(LerpCamera(transform.position.z ,directionValue, CameraMoveLerpScale, true));
                break;
            case "East":
                directionValue.y = transform.position.z;
                transform.Translate(LerpCamera(transform.position.x ,directionValue, CameraMoveLerpScale, false));
                break;
            case "West":
                directionValue.y = transform.position.z;
                transform.Translate(LerpCamera(transform.position.x ,directionValue, CameraMoveLerpScale, false));
                break;
                
                
                
        }
    }
    
    public void SetFollowParent(Transform parentTransform)
    {
        //_parentTransform = parentTransform;
    }

    Vector3 GetViewRightDir()
    {
        return viewCamera.transform.right;
    }

    Vector3 GetViewUpDir()
    {
        return Vector3.Cross(GetViewRightDir(), Vector3.up);
    }

    public Vector3 InputToWorldDir(Vector2 input)
    {
        return GetViewRightDir() * input.x + GetViewUpDir() * input.y;
    }

    private Vector3 LerpCamera(float oldLocation, Vector2 newLocation, float time, bool direction)
    {
        if (direction)
        {
            Vector3 newPosition = transform.position;
            newPosition.z =oldLocation + (newLocation.y - oldLocation) * time;

            return newPosition;
        }
        else
        {
            Vector3 newPosition = transform.position;
            newPosition.x =oldLocation + (newLocation.x - oldLocation) * time;

            return newPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        viewCamera.transform.position = pitchTransform.position - viewCamera.transform.forward * armLength;
    }

    private void LateUpdate()
    {
        if (_parentTransform is null)
            return;
        
        transform.position = _parentTransform.position;
    }

    public void AddYawInput(float amt)
    {
        transform.Rotate(Vector3.up, amt * Time.deltaTime * cameraTurnSpeed);
    }
}
