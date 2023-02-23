using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nrjwolf.Tools.AttachAttributes;

public class Player : MonoBehaviour
{
    [GetComponent] [SerializeField] PlayerMotor _playerMotor;
    [GetComponentInChildren()] [SerializeField] Animator _animator;

    void OnEnable()
    {
        _playerMotor.OnMove += HandleMove;
    }

    void OnDisable()
    {
        _playerMotor.OnMove += HandleMove;
    }

    void HandleMove(bool isWalking)
    {
        _animator.SetBool("IsWalking", isWalking);
    }
}
