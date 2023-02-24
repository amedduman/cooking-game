using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public event Action OnInteractButtonPressed;
    PlayerInputActions _playerInputActions;

    void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Interact.performed += HandleInteractAction;
    }

    void HandleInteractAction(InputAction.CallbackContext obj)
    {
        OnInteractButtonPressed?.Invoke();        
    }

    public Vector2 GetMovementVectorNormalized()
    {
         Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector.Normalize();
        return inputVector;
    }
}
