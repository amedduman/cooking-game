using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
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

    [CanBeNull]
    public KitchenObject PickKitchenObject(KitchenObject kitchenObject)
    {
        if (MyKitchenObject == null)
        {
            MyKitchenObject = kitchenObject;
            PutKitchenObjectToPos();
            return null;
        }
        else
        {
            var holdKitchenObj = MyKitchenObject;
            MyKitchenObject = kitchenObject;
            PutKitchenObjectToPos();
            return holdKitchenObj;
        }
    }

    [CanBeNull]
    public KitchenObject DropKitchenObject()
    {    
        var kitchenObj = MyKitchenObject;
        MyKitchenObject = null;
        return kitchenObj;
    }

    void PutKitchenObjectToPos()
    {
        MyKitchenObject.transform.parent = _kitchenObjectPos;
        MyKitchenObject.transform.DOLocalMove(Vector3.zero, .2f);
    }
}
