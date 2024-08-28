using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private GamePlayWidget gameplayWidgetPrefab;
    [SerializeField] private float _speed = 10;
    private GamePlayWidget _gamePlayWidget;
    private CharacterController _characterController;
    
    private Animator _animator;
    private Vector2 _moveInput;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _gamePlayWidget = Instantiate(gameplayWidgetPrefab);
        _gamePlayWidget.MoveStick.OnInputUpdated += InputUpdated;
    }

    private void InputUpdated(Vector2 inputVal)
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
        _characterController.Move(new Vector3(_moveInput.x, 0f, _moveInput.y) * (_speed * Time.deltaTime));
    }
}
