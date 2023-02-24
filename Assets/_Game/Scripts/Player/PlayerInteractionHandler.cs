using System;
using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour
{
    [SerializeField] [Min(2)] float _interactionRayMaxLength = 2;
    Vector3 _lastAttemptedMoveDir;
    LayerMask _counterLayerMask;
    Transform _tr;

    void Awake()
    {
        _tr = transform;
        _counterLayerMask = LayerMask.GetMask("Counter");
    }

    public void HandleInteractions(Vector3 moveDir)
    {
        if (moveDir != Vector3.zero)
        {
            _lastAttemptedMoveDir = moveDir;
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