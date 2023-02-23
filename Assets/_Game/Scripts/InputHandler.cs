using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Vector2 GetMovementVector()
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
        return inputVector;
    }
}
