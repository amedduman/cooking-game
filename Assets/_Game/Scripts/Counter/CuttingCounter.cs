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
            _myKitchenObj = _player.DropKitchenObject();
            PutKitchenObjToPos();
        }
        else
        {
            Debug.Log("log");

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

    public void Slice()
    {
        if (_myKitchenObj == null) return;
        if (_myKitchenObj.IsSliceable)
        {
            _myKitchenObj.Slice();
        }
        else
        {
            Debug.LogError("system shouldn't allow call slice function on non-sliceable kitchen objects");
        }
    }

    public override bool IsCounterAvailableToInteract(Player player)
    {
        if (_player.MyKitchenObject == null)
        {
            if (_myKitchenObj != null)
            {
                if (_myKitchenObj.IsSliced)
                {
                    return true;
                }

                return false;
            }

            return false;
        }
        else
        {
            if (_player.MyKitchenObject.IsSliceable)
            {
                if (_myKitchenObj == null)
                {
                    return true;
                }
                else
                {
                    if (_myKitchenObj.IsSliced)
                    {
                        return true;
                    }

                    return false;
                }
            }

            return false;
        }
    }
    
    void PutKitchenObjToPos()
    {
        _myKitchenObj.transform.parent = _kitchenObjectPoint;
        _myKitchenObj.transform.DOLocalMove(Vector3.zero, .2f);
    }
}
