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
            var takenKitchenObj = _player.DropKitchenObject();
            if (takenKitchenObj == null) return;
            if (!takenKitchenObj.IsSliceable) return;
            _myKitchenObj = takenKitchenObj;
            PutKitchenObjToPos();
            return;
        }
        
        
        
        if (_myKitchenObj.IsSliced)
        {
            if (_player.MyKitchenObject != null)
            {
                if (_player.MyKitchenObject.IsSliceable)
                {
                    var newKitchenObj = _player.MyKitchenObject;
                    _player.PickKitchenObject(_myKitchenObj);
                    _myKitchenObj = newKitchenObj;
                    PutKitchenObjToPos();
                }
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
        _myKitchenObj.Slice();
    }

    public void Slice()
    {
        if (_myKitchenObj.IsSliceable)
        {
            _myKitchenObj.Slice();
        }
        else
        {
            Debug.LogError("system shouldn't allow call slice function on non-sliceable kitchen objects");
        }
    }

    void PutKitchenObjToPos()
    {
        _myKitchenObj.transform.parent = _kitchenObjectPoint;
        _myKitchenObj.transform.DOLocalMove(Vector3.zero, .2f);
    }
}
