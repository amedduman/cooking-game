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
    Transform _tr;

    void Awake()
    {
        _tr = transform;
    }

    void Update()
    {
        Vector2 inputVector = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
        }
        inputVector.Normalize();
        
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        _tr.position += moveDir * (Time.deltaTime * _speed);

        Vector3 rotDir = _tr.forward;
        rotDir = Vector3.Slerp(rotDir, moveDir, Time.deltaTime * _rotSpeed);
        if (moveDir.sqrMagnitude > 0 + Mathf.Epsilon)
        {
            _tr.forward = rotDir;
           OnMove?.Invoke(true);       
        }
        else
        {
            OnMove?.Invoke(false);       
        }
    }
}
