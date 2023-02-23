using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] float _speed = 5;
    [SerializeField] float _rotSpeed = 5;

    public event Action<bool> OnMove;
    InputHandler _inputHandler;
    Transform _tr;

    void Awake()
    {
        _tr = transform;
    }

    void Start()
    {
        _inputHandler = ServiceLocator.Get<InputHandler>();
    }

    void Update()
    {
        Vector2 inputVector = _inputHandler.GetMovementVectorNormalized();

        if (inputVector == Vector2.zero)
        {
            OnMove?.Invoke(false);
            return;
        }
        
        OnMove?.Invoke(true);
        
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y).normalized;
        
        Move(moveDir);

        Rotate(moveDir);
    }

    void Move(Vector3 moveDir)
    {
        _tr.position += moveDir * (Time.deltaTime * _speed);
    }
    
    void Rotate(Vector3 moveDir)
    {
        Vector3 rotDir = _tr.forward;
        rotDir = Vector3.Slerp(rotDir, moveDir, Time.deltaTime * _rotSpeed);
        _tr.forward = rotDir;
    }
}
