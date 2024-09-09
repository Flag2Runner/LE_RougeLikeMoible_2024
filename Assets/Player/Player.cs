using System;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SocketManager))]
[RequireComponent(typeof(InventoryComponent))]
public class Player : MonoBehaviour
{
    [SerializeField] private GamePlayWidget gameplayWidgetPrefab;
    [SerializeField] private float speed = 10;
    [SerializeField] private float bodyTurnSpeed = 10f;
    [SerializeField] private ViewCamera viewCameraPrefab;
    [SerializeField] private float animTurnLerpScale = 5f;
    
    private GamePlayWidget _gamePlayWidget;
    private CharacterController _characterController;
    private ViewCamera _viewCamera;
    
    private Animator _animator;
    private float _animTurnSpeed;
    private Vector2 _moveInput;
    private Vector2 _aimImput;

    private static int animFwdId = Animator.StringToHash("ForwardAmt");
    private static int animRightrdId = Animator.StringToHash("RightAmt");
    private static int animTurnId = Animator.StringToHash("TurnAmt");


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

        float angleDelta = 0f;
        if (aimDir != Vector3.zero)
        {
            Vector3 prevDir = transform.forward;
            Quaternion goalRot = Quaternion.LookRotation(aimDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, goalRot, Time.deltaTime * bodyTurnSpeed);
            angleDelta = Vector3.SignedAngle(transform.forward,prevDir, Vector3.up);
        }

        _animTurnSpeed = Mathf.Lerp(_animTurnSpeed, angleDelta / Time.deltaTime, Time.deltaTime * animTurnLerpScale);
        _animator.SetFloat(animTurnId, _animTurnSpeed);

        float animFwdAmt = Vector3.Dot(moveDir, transform.forward);
        float animRightAmt = Vector3.Dot(moveDir, transform.right);
        
        _animator.SetFloat(animFwdId, animFwdAmt);
        _animator.SetFloat(animRightrdId, animRightAmt);

    }
}
