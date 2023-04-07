using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public event Action OnInteractButtonPressed;
    public event Action OnInteractAlternateButtonPressed;
    PlayerInputActions _playerInputActions;

    void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Interact.performed += HandleInteractAction;
        _playerInputActions.Player.InteractAlternate.performed += HandleInteractAlternateAction;
    }

    public void DisableInput()
    {
        _playerInputActions.Disable();
    }

    void HandleInteractAction(InputAction.CallbackContext obj)
    {
        OnInteractButtonPressed?.Invoke();        
    }
    
    void HandleInteractAlternateAction(InputAction.CallbackContext obj)
    {
        OnInteractAlternateButtonPressed?.Invoke();
    }

    public Vector2 GetMovementVectorNormalized()
    {
         Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector.Normalize();
        return inputVector;
    }
}
