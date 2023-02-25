using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Nrjwolf.Tools.AttachAttributes;

public class Player : MonoBehaviour
{
    [GetComponent] [SerializeField] PlayerMotor _playerMotor;
    [GetComponentInChildren()] [SerializeField] Animator _animator;
    [SerializeField] Transform _kitchenObjectPos;
    public KitchenObject MyKitchenObject { get; private set; }
    static readonly int walking = Animator.StringToHash("IsWalking");
    
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
        _animator.SetBool(walking, isWalking);
    }

    public void PickKitchenObject(KitchenObject kitchenObject)
    {
        if (MyKitchenObject == null)
        {
            MyKitchenObject = kitchenObject;
            MyKitchenObject.transform.parent = _kitchenObjectPos;
            MyKitchenObject.transform.DOLocalMove(Vector3.zero, .2f);
        }
    }
}
