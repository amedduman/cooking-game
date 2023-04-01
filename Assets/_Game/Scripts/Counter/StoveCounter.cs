using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : Counter
{
    KitchenObject _myKitchenObj;

    public override void Interact()
    {
        if (_myKitchenObj == null)
        {
            if (_player.MyKitchenObject == null) return;
            if (_player.MyKitchenObject.IsCookable == false) return;
            if (_player.MyKitchenObject.MyCookingState != KitchenObject.cookingState.Raw) return;

            var takenKitchenObj = _player.DropKitchenObject();
            _myKitchenObj = takenKitchenObj;
            PutKitchenObjToPos(_myKitchenObj);

            return;
        }
    }
}
