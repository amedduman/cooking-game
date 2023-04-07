using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryUI : MonoBehaviour
{
    [SerializeField] OrderUI _orderUIprefab;
    [SerializeField] Transform _orderUIparent;

    //public void AddOrder(Recipe order)
    //{
    //    OrderUI orderUI = Instantiate(_orderUIprefab, _orderUIparent);
    //    orderUI.SetOrderName(order.RecipeName);
    //}

    //public void RemoveOrder()
    //{
    //    if(_orderUIparent.GetChild(0))
    //        Destroy(_orderUIparent.GetChild(0).gameObject);
    //}

    public void UpdateWaitingOrderList(List<OrderInfo> waitingOrders)
    {
        DestroyAllChildObjects(transform);

        foreach (var order in waitingOrders)
        {
            OrderUI orderUI = Instantiate(_orderUIprefab, _orderUIparent);
            orderUI.SetOrderUI(order.MyRecipe.RecipeName, order.IsBeingPrepared);
        }
    }

    void DestroyAllChildObjects(Transform parent)
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }

}
