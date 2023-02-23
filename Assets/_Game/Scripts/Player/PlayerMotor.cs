using System;
using System.Collections;
using System.Collections.Generic;
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
        
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        
        if (!CanMove(ref moveDir)) return;
        
        moveDir.Normalize();
        
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

    bool CanMove(ref Vector3 moveDir)
    {
        bool canMove = false;
        canMove = !Physics.SphereCast(_tr.position, _collisionDetectionSphereRadius, moveDir, out RaycastHit hitInfo,
            _collisionDetectionRayMaxLength);
        if (canMove) return true;

        if (!Mathf.Approximately(moveDir.x, 0))
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            Debug.Log("red");
            Debug.DrawLine(_tr.position, _tr.position + moveDirX, Color.red);
            canMove = !Physics.SphereCast(_tr.position, _collisionDetectionSphereRadius, moveDirX, out RaycastHit hitInfo2,
                _collisionDetectionRayMaxLength);
            if(canMove) moveDir = moveDirX;
            
        }

        if (canMove) return true;
        
        if (!Mathf.Approximately(moveDir.z, 0))
        {
            Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
            Debug.DrawLine(_tr.position, _tr.position + moveDirZ, Color.green);
            canMove = !Physics.SphereCast(_tr.position, _collisionDetectionSphereRadius, moveDirZ, out RaycastHit hitInfo2,
                _collisionDetectionRayMaxLength);
            if(canMove) moveDir = moveDirZ;
        }
        
        return canMove;
    }

    void OnDrawGizmos()
    {
        // draw collision detection sphere        
        _tr = transform;
        Gizmos.color = Color.red;
        // Gizmos.DrawSphere(_tr.position, _collisionDetectionSphereRadius);
    }
}
