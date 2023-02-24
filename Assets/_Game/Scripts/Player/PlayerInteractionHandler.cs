using System;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour
{
    [GetComponent] [SerializeField] PlayerMotor _playerMotor;
    [SerializeField] [Min(2)] float _interactionRayMaxLength = 2;
    InputHandler _inputHandler;
    Vector3 _lastAttemptedMoveDir;
    LayerMask _counterLayerMask;
    Transform _tr;

    void Awake()
    {
        _tr = transform;
        _inputHandler = ServiceLocator.Get<InputHandler>();
        _counterLayerMask = LayerMask.GetMask("Counter");
    }

    void OnEnable()
    {
        _inputHandler.OnInteractButtonPressed += HandleInteractions;
    }

    void OnDisable()
    {
        _inputHandler.OnInteractButtonPressed += HandleInteractions;
    }

    void HandleInteractions()
    {
        Vector3 attemptedMoveDir = _playerMotor.GetAttemptedMoveDir();
        
        if (attemptedMoveDir != Vector3.zero)
        {
            _lastAttemptedMoveDir = attemptedMoveDir;
        }
        
        if (Physics.Raycast(_tr.position, _lastAttemptedMoveDir,
                out RaycastHit hitInfo, _interactionRayMaxLength, _counterLayerMask))
        {
            if (hitInfo.transform.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }        
}