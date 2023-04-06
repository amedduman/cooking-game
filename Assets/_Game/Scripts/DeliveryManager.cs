using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    [SerializeField] Recipe[] _possibleRecipes;
    [SerializeField] float _timeBetweenEachOrder = 4;
    [SerializeField] int _maxNumberOfOrdersCanbeAtTheSameTime = 2;
    [SerializeField] int _totalNumberOfOrdersToPrepare = 3;
    [SerializeField] int _timeToCompleteOrders = 10;
    int _createdOrderNumber;
    int _readyOrderCount;
    float _gameTimer;

    Queue<Recipe> _orders = new();
    DeliveryUI _deliveryUI;

    private void Start()
    {
        _deliveryUI = ServiceLocator.Get<DeliveryUI>();
        StartCoroutine(AddToOrderQueue());
        StartCoroutine(GameTimer());
    }

    IEnumerator GameTimer()
    {
        while(ShouldTimerRun())
        {
            _gameTimer += Time.deltaTime;
            ServiceLocator.Get<GameTimerCanvas>().UpdateTheTimer(_timeToCompleteOrders - (int)_gameTimer);
            if ((int)_gameTimer >= _timeToCompleteOrders)
            {
                if (_readyOrderCount < _totalNumberOfOrdersToPrepare)
                {
                    Debug.Log("lose");
                }
            }

            yield return null;
        }

        bool ShouldTimerRun()
        {
            if(_readyOrderCount < _totalNumberOfOrdersToPrepare)
            {
                return (int)_gameTimer < _timeToCompleteOrders;
            }
            return false;
        }
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
            _readyOrderCount++;
            Destroy(kitchenObj.gameObject);

            if (_readyOrderCount == _totalNumberOfOrdersToPrepare)
            {
                Debug.Log("win");
            }

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
