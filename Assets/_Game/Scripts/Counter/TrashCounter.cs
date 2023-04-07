using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : Counter
{
    public override void Interact()
    {
        if(_player.MyKitchenObject != null)
        {
            if(_player.MyKitchenObject.IsPlate)
            {
                ServiceLocator.Get<DeliveryManager>().OrderTrashed(_player.MyKitchenObject.MyRecipe);
            }
            _player.DestroyHoldingKitchenObject();
        }
    }
}
