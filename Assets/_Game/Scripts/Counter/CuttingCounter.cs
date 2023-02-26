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
            if (_myKitchenObj.IsSliced)
            {
                _player.PickKitchenObject(_myKitchenObj);
                _myKitchenObj = null;
            }
        }
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
