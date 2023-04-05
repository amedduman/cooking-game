using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryUI : MonoBehaviour
{
    [SerializeField] OrderUI _orderUIprefab;
    [SerializeField] Transform _orderUIparent;

    public void UpdateOrders(Recipe order)
    {
        OrderUI orderUI = Instantiate(_orderUIprefab, _orderUIparent);
        orderUI.SetOrderName(order.RecipeName);
    }
}
