using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] Recipe[] _possibleRecipes;
    [SerializeField] float _timeBetweenEachOrder = 4;
    [SerializeField] int _maxNumberOfOrdersCanbeAtTheSameTime = 2;
    public Queue<Recipe> _orders = new();
    DeliveryUI _deliveryUI;

    private void Start()
    {
        _deliveryUI = ServiceLocator.Get<DeliveryUI>();
        StartCoroutine(AddToOrderQueue());
    }

    IEnumerator AddToOrderQueue()
    {
        while (true)
        {
            if(_orders.Count < _maxNumberOfOrdersCanbeAtTheSameTime)
            {
                var order = _possibleRecipes[Random.Range(0, _possibleRecipes.Length)];
                _orders.Enqueue(order);
                _deliveryUI.UpdateOrders(order);
                yield return new WaitForSecondsRealtime(_timeBetweenEachOrder);
            }

            yield return null;
        }
    }
}
