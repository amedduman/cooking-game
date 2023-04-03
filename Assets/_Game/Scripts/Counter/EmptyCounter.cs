using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EmptyCounter : Counter
{
    KitchenObject _myKitchenObj;
    
    public override void Interact()
    {
        if (_myKitchenObj != null)
        {
            if (_myKitchenObj.IsPlate)
            {
                var result = _myKitchenObj.MyRecipe.TryToAddIngredient(_player.MyKitchenObject);
                if (result)
                {
                    _player.ResetPlayerHoldingObject();
                }
            }
            else
            {
                var returnedKitchenObj = _player.PickKitchenObject(_myKitchenObj);
                if (returnedKitchenObj == null)
                {
                    _myKitchenObj = null;
                }
                else
                {
                    _myKitchenObj = returnedKitchenObj;
                    PutKitchenObjToPos();
                }
            }
        }
        else
        {
            var returnedKitchenObj = _player.DropKitchenObject();
            if (returnedKitchenObj == null) return;
            _myKitchenObj = returnedKitchenObj;
            PutKitchenObjToPos();
        }
    }

    void PutKitchenObjToPos()
    {
        _myKitchenObj.transform.parent = _kitchenObjectPoint;
        _myKitchenObj.transform.DOLocalMove(Vector3.zero, .2f);
    }
}
