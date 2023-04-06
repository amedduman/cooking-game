using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] Recipe[] _possibleRecipes;
    [SerializeField] float _timeBetweenEachOrder = 4;
    [SerializeField] int _maxNumberOfOrdersCanbeAtTheSameTime = 2;
    [SerializeField] int _totalNumberOfOrdersToPrepare = 3;
    int _createdOrderNumber;
    public Queue<Recipe> _orders = new();
    DeliveryUI _deliveryUI;

    private void Start()
    {
        _deliveryUI = ServiceLocator.Get<DeliveryUI>();
        StartCoroutine(AddToOrderQueue());
    }

    IEnumerator AddToOrderQueue()
    {
        while (_createdOrderNumber < _totalNumberOfOrdersToPrepare)
        {
            if(_orders.Count < _maxNumberOfOrdersCanbeAtTheSameTime)
            {
                var order = _possibleRecipes[Random.Range(0, _possibleRecipes.Length)];
                _orders.Enqueue(order);
                _deliveryUI.AddOrder(order);
                _createdOrderNumber++;

                yield return new WaitForSecondsRealtime(_timeBetweenEachOrder);
            }
            yield return null;
        }
    }

    public Recipe GetTopOrder()
    {
        _deliveryUI.RemoveOrder();
        return _orders.Dequeue();
    }

    public bool HasOrderNotInProgress()
    {
        return _orders.Count > 0;
    }

    public bool EvaluateRecipe(KitchenObject kitchenObj)
    {
        DebugCode();
        if(kitchenObj.MyRecipe.MyCompletionStatus.IsCompleted)
        {
            Debug.Log("completed meal");
            Destroy(kitchenObj.gameObject);
            return true;
        }
        return false;

        void DebugCode()
        {
            if (kitchenObj.IsPlate)
            {
                if (kitchenObj.MyRecipe)
                {
                }
                else
                {
                    Debug.Log("the plate doesn't have recipe. Thsi shouln't happen");
                }
            }
            else
            {
                Debug.Log("kitchen obj is not plate. this shouldn't happen");
            }
        }
    }
}
