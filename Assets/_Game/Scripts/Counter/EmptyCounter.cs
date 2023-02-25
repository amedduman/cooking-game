using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCounter : Counter
{
    [SerializeField] KitchenObjectSO _kitchenObjectSO;
    [SerializeField] Vector3 _spawningPoint;
    KitchenObject _myKitchenObj;
    

    public override void Interact()
    {
        if (_myKitchenObj == null)
        {
            _myKitchenObj = Instantiate(_kitchenObjectSO.Prefab, GetSpawnPoint(), Quaternion.identity);
            _myKitchenObj.Counter = this;
        }
        else
        {
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(GetSpawnPoint(), .1f);
    }

    Vector3 GetSpawnPoint()
    {
        return transform.position + _spawningPoint;
    }
}
