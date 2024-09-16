using System;
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
    [SerializeField] private float CameraTurnLerpScale = 5f;

    private Transform _parentTransform;

    public Camera GetViewCamera()
    {
        return viewCamera;
    }

    public void MoveCamera(Vector2 value)
    {
        viewCamera.transform.position.Set(value.x, 0, value.x);   
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
