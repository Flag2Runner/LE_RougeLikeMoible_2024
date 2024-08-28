using System;
using UnityEngine;
[ExecuteAlways]

public class ViewCamera : MonoBehaviour
{

    [SerializeField] private Transform pitchTransform;
    [SerializeField] private Camera viewCamera;
    [SerializeField] private float armLength = 7f;

    private Transform _parentTransform;

    public void SetFollowParent(Transform parentTransform)
    {
        _parentTransform = parentTransform;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
