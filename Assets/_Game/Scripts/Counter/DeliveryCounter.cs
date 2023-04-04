using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : Counter
{
    public override void Interact()
    {
        if (_player.MyKitchenObject == null) return;
        if (_player.MyKitchenObject.IsPlate)
        {
            var returnedObj = _player.DropKitchenObject();
            PutKitchenObjToPos(returnedObj);
        }
    }
}
