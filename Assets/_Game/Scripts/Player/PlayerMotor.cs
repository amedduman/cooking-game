using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] float _speed = 5;
    [SerializeField] float _rotSpeed = 5;
    [SerializeField] [Min(.5f)] float _collisionDetectionRayMaxLength = .7f;
    [SerializeField] [Min(.5f)] float _collisionDetectionSphereRadius = .7f;
    public event Action<bool> OnMove;
    InputHandler _inputHandler;
    Transform _tr;

    #region UnityCallbacks

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
        
        Vector3 attemptedMoveDir = new Vector3(inputVector.x, 0, inputVector.y);

        Vector3 normalizedMoveDir = GetMoveableDirectionIfAny(attemptedMoveDir).normalized;
        if(normalizedMoveDir == Vector3.zero) return;
        
        Move(normalizedMoveDir);

        Rotate(normalizedMoveDir);
    }

    #endregion
    

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

    Vector3 GetMoveableDirectionIfAny(Vector3 moveDir)
    {
        if(!CastWithCollider(moveDir)) return moveDir;

        if (!Mathf.Approximately(moveDir.x, 0))
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            if(!CastWithCollider(moveDirX)) return moveDirX;
        }

        if (!Mathf.Approximately(moveDir.z, 0))
        {
            Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
            if(!CastWithCollider(moveDirZ)) return moveDirZ;
        }
        
        return Vector3.zero;

        bool CastWithCollider(Vector3 dir)
        {
            return Physics.SphereCast(_tr.position, _collisionDetectionSphereRadius, dir, out RaycastHit hitInfo2,
                _collisionDetectionRayMaxLength);
        }
    }
}
