using System;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour
{
    [GetComponent] [SerializeField] PlayerMotor _playerMotor;
    [GetComponent] [SerializeField] Player _player;
    [SerializeField] [Min(2)] float _interactionRayMaxLength = 2;
    InputHandler _inputHandler;
    Vector3 _lastAttemptedMoveDir;
    LayerMask _counterLayerMask;
    Transform _tr;
    bool _hasInteractButtonPressed;
    bool _hasInteractAlternateButtonPressed;
    
    void Awake()
    {
        _tr = transform;
        _inputHandler = ServiceLocator.Get<InputHandler>();
        _counterLayerMask = LayerMask.GetMask("Counter");
    }
    
    void OnEnable()
    {
        _inputHandler.OnInteractButtonPressed += HandleInteractButtonPressed;
        _inputHandler.OnInteractAlternateButtonPressed += HandleInteractAlternateButtonPressed;
    }

    void OnDisable()
    {
        _inputHandler.OnInteractButtonPressed -= HandleInteractButtonPressed;
        _inputHandler.OnInteractAlternateButtonPressed -= HandleInteractAlternateButtonPressed;
    }

    void Update()
    {
        Interact();
    }
    
    void HandleInteractButtonPressed()
    {
        _hasInteractButtonPressed = true;
    }
    
    void HandleInteractAlternateButtonPressed()
    {
        _hasInteractAlternateButtonPressed = true;
    }

    void Interact()
    {
        Vector3 attemptedMoveDir = _playerMotor.GetAttemptedMoveDir();
        
        if (attemptedMoveDir != Vector3.zero)
        {
            _lastAttemptedMoveDir = attemptedMoveDir;
        }
        
        if (Physics.Raycast(_tr.position, _lastAttemptedMoveDir,
                out RaycastHit hitInfo, _interactionRayMaxLength, _counterLayerMask))
        {
            if (hitInfo.transform.TryGetComponent(out Counter counter))
            {
                counter.Highlight();

                if (_hasInteractAlternateButtonPressed)
                {
                    if (_player.MyKitchenObject != null) return; // if player hold something cant interact with cutting counter
                    if (counter.TryGetComponent(out CuttingCounter cuttingCounter))
                    {
                        cuttingCounter.Slice();
                    }

                    _hasInteractAlternateButtonPressed = false;
                }
                
                if (!_hasInteractButtonPressed) return;

                if (!counter.IsCounterAvailableToInteract(_player)) return;
                
                counter.Interact();
            }
        }

        _hasInteractButtonPressed = false;
    }        
}