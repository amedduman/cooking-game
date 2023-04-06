using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : Counter
{
    KitchenObject _myKitchenObj;

    public override void Interact()
    {
        if (_player.MyKitchenObject != null)
        {
            HandlePlayerHasKitchenObj();
        }
        else
        {
            HandlePlayerIsEmptyHanded();
        }
        
    }

    void HandlePlayerHasKitchenObj()
    {
        KitchenObject returnedObj;
        if (_player.MyKitchenObject.IsPlate)
        {
            returnedObj = _player.DropKitchenObject();
            PutKitchenObjToPos(returnedObj, WhenObjIsOnDeliveryCounter);
        }

        void WhenObjIsOnDeliveryCounter()
        {
            _myKitchenObj = returnedObj;
            var result = ServiceLocator.Get<DeliveryManager>().EvaluateRecipe(_myKitchenObj);
            if (result)
            {
                _myKitchenObj = null;
            }
        }
    }

    void HandlePlayerIsEmptyHanded()
    {
        if(_myKitchenObj != null)
        {
            if (_player.MyKitchenObject == null)
            {
                _player.PickKitchenObject(_myKitchenObj);
                _myKitchenObj = null;
            }
        }
    }

}
