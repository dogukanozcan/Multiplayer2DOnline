using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public Action<Vector2> OnMoveInputChanged;

    [SerializeField] private float _moveSpeed = 1;
    private Vector2 _input;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        
    }

    private void OnEnable()
    {
        GameNetworkManager.Instance.OnGameEnd += OnGameEnd;
    }

    private void OnDisable()
    {
        GameNetworkManager.Instance.OnGameEnd -= OnGameEnd;
    }

    private void OnGameEnd()
    {
        enabled = false;
    }

    private void Update()
    {
        if (!IsOwner) return;
      
        MoveInput();
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;

        Move();
    }

    private void MoveInput()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            var dir = MobileInputManager.Instance.joystick.Direction;
            _input.x = dir.x;
            _input.y = dir.y;
        }
        else
        {
            _input.x = Input.GetAxisRaw("Horizontal");
            _input.y = Input.GetAxisRaw("Vertical");
        }
       
        //prevent diagonal move
        //if (_input.x != 0) _input.y = 0;

        OnMoveInputChanged?.Invoke(_input);
    }

    private void Move()
    {
        if (!_input.Equals(Vector2.zero))
        {
            var targetPos = GetTargetPos();
            _rigidbody.MovePosition(targetPos);
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }

    
    private Vector3 GetTargetPos()
    {
        var targetPos = transform.position;
        _input = _input.normalized;
      
        targetPos.x += _input.x * _moveSpeed/10f;
        targetPos.y += _input.y * _moveSpeed/10f;
       
        return targetPos;
    }

  
}
