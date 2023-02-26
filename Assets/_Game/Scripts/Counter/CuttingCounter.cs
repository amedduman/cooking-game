using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CuttingCounter : Counter
{
    KitchenObject _myKitchenObj;

    public override void Interact()
    {
        if (_myKitchenObj == null)
        {
            if (_player.MyKitchenObject == null) return;
            if (!_player.MyKitchenObject.IsSliceable) return;
            if (_player.MyKitchenObject.IsSliced) return;

            var takenKitchenObj = _player.DropKitchenObject();
            _myKitchenObj = takenKitchenObj;
            PutKitchenObjToPos();
            
            return;
        }
        
        if (_myKitchenObj.IsSliced)
        {
            if (_player.MyKitchenObject != null)
            {
                if (_player.MyKitchenObject.IsSliced) return;
                if (!_player.MyKitchenObject.IsSliceable) return;
                
                var newKitchenObj = _player.MyKitchenObject;
                _player.PickKitchenObject(_myKitchenObj);
                _myKitchenObj = newKitchenObj;
                PutKitchenObjToPos();
            }
            else
            {
                _player.PickKitchenObject(_myKitchenObj);
                _myKitchenObj = null;
            }
                
        }
        else
        {
            if (_myKitchenObj.CurrentHitCount == 0)
            {
                if (_player.MyKitchenObject == null)
                {
                    _player.PickKitchenObject(_myKitchenObj);
                    _myKitchenObj = null;
                }
                else
                {
                    if (_player.MyKitchenObject.IsSliceable)
                    {
                        var newKitchenObj = _player.MyKitchenObject;
                        _player.PickKitchenObject(_myKitchenObj);
                        _myKitchenObj = newKitchenObj;
                        PutKitchenObjToPos();
                    }
                }
            }
        }
    }

    public override void InteractAlternate()
    {
        if (_myKitchenObj == null) return;
        
        if (_myKitchenObj.IsSliceable)
        {
            _myKitchenObj.Slice();
        }
        else
        {
            Debug.LogError("system shouldn't allow call slice function on non-sliceable kitchen objects. because system shouldn't allow put non slice-able object in the cutting counter");
        }
    }

    void PutKitchenObjToPos()
    {
        _myKitchenObj.transform.parent = _kitchenObjectPoint;
        _myKitchenObj.transform.DOLocalMove(Vector3.zero, .2f);
    }
}
