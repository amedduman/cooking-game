using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryUI : MonoBehaviour
{
    [SerializeField] OrderUI _orderUIprefab;
    [SerializeField] Transform _orderUIparent;

    public void UpdateWaitingOrderList(List<OrderInfo> waitingOrders)
    {
        DestroyAllChildObjects(transform);

        List<OrderInfo> preparingOrders = new();
        List<OrderInfo> notPreparingOrders = new();

        foreach (var order in waitingOrders)
        {
            if(order.IsBeingPrepared)
            {
                preparingOrders.Add(order);
            }
            else
            {
                notPreparingOrders.Add(order);
            }
        }

        foreach (var order in preparingOrders)
        {
            OrderUI orderUI = Instantiate(_orderUIprefab, _orderUIparent);
            orderUI.SetOrderUI(order.MyRecipe.RecipeName, order.IsBeingPrepared);
        }

        foreach (var order in notPreparingOrders)
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
