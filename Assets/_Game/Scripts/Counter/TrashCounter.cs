using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : Counter
{
    public override void Interact()
    {
        if(_player.MyKitchenObject != null)
            _player.DestroyHoldingKitchenObject();
        else
        {
            Debug.Log("not null");
        }
    }
}
