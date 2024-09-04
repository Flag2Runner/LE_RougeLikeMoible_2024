using System;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private GamePlayWidget gameplayWidgetPrefab;
    [SerializeField] private float speed = 10;
    [SerializeField] private float bodyTurnSpeed = 10f;
    [SerializeField] private ViewCamera viewCameraPrefab;
    private GamePlayWidget _gamePlayWidget;
    private CharacterController _characterController;
    private ViewCamera _viewCamera;
    
    private Animator _animator;
    private Vector2 _moveInput;
    private Vector2 _aimImput;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _gamePlayWidget = Instantiate(gameplayWidgetPrefab);
        _gamePlayWidget.MoveStick.OnInputUpdated += MoveInputUpdated;
        _gamePlayWidget.AimStick.OnInputUpdated += AimInputUpdated;
       // _gamePlayWidget.ViewStick.OnInputUpdated += ViewInputUpdated;
        _viewCamera = Instantiate(viewCameraPrefab);
        _viewCamera.SetFollowParent(transform);
    }
   private void AimInputUpdated(Vector2 inputVal)
    {
        _aimImput = inputVal;
    }
    private void MoveInputUpdated(Vector2 inputVal)
    {
        _moveInput = inputVal;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = _viewCamera.InputToWorldDir(_moveInput);
        _characterController.Move(moveDir * (speed * Time.deltaTime));

        Vector3 aimDir = _viewCamera.InputToWorldDir(_aimImput);
        if (aimDir == Vector3.zero)
        {
            aimDir = moveDir;
            _viewCamera.AddYawInput(_moveInput.x);
        }

        if (aimDir != Vector3.zero)
        {
            Quaternion goalRot = Quaternion.LookRotation(aimDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, goalRot, Time.deltaTime * bodyTurnSpeed);
        }
      
    }
}
