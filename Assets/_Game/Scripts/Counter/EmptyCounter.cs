using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCounter : Counter
{
    protected KitchenObject _myKitchenObj;
    

    public override void Interact()
    {
        
    }

    public override bool IsCounterAvailableToInteract(Player player)
    {
        return true;
    }
}
