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
    int _givenOrderNumber;
    int _deliveredOrderCount;
    float _gameTimer;

    List<OrderInfo> _waitingOrders = new();
    DeliveryUI _deliveryUI;

    #region mb
    private void Start()
    {
        _deliveryUI = ServiceLocator.Get<DeliveryUI>();
        StartCoroutine(GenerateWaitingOrderList());
        StartCoroutine(GameTimer());
    }
    #endregion

    #region private
    IEnumerator GameTimer()
    {
        while (ShouldTimerRun())
        {
            _gameTimer += Time.deltaTime;
            ServiceLocator.Get<GameTimerCanvas>().UpdateTheTimer(_timeToCompleteOrders - (int)_gameTimer);
            if ((int)_gameTimer >= _timeToCompleteOrders)
            {
                if (_deliveredOrderCount < _totalNumberOfOrdersToPrepare)
                {
                    Debug.Log("lose");
                }
            }

            yield return null;
        }

        bool ShouldTimerRun()
        {
            if (_deliveredOrderCount < _totalNumberOfOrdersToPrepare)
            {
                return (int)_gameTimer < _timeToCompleteOrders;
            }
            return false;
        }
    }

    IEnumerator GenerateWaitingOrderList()
    {
        while (_givenOrderNumber < _totalNumberOfOrdersToPrepare)
        {
            if (_waitingOrders.Count < _maxNumberOfOrdersCanbeAtTheSameTime)
            {
                var recipe = _possibleRecipes[Random.Range(0, _possibleRecipes.Length)];
                var order = new OrderInfo(recipe);
                _waitingOrders.Add(order);
                _deliveryUI.UpdateWaitingOrderList(_waitingOrders);
                _givenOrderNumber++;

                yield return new WaitForSecondsRealtime(_timeBetweenEachOrder);
            }
            yield return null;
        }
    }
    #endregion

    #region public
    public OrderInfo GetTopOrder()
    {
        OrderInfo topOrder = null;

        foreach (var order in _waitingOrders)
        {
            if (order.IsBeingPrepared == false)
            {
                topOrder = order;
                break;
            }
        }

        #region DebugCode
        if (topOrder == null)
        {
            Debug.Log("waiting order list is null but you are requesting for order. this behaviour shouldn't happen!");
            return null;
        }
        #endregion
        topOrder.IsBeingPrepared = true;
        _deliveryUI.UpdateWaitingOrderList(_waitingOrders);
        return topOrder;
    }

    public void OrderTrashed(Recipe recipe)
    {
        foreach (var order in _waitingOrders)
        {
            if (order.IsBeingPrepared)
            {
                if (order.MyRecipe.RecipeName == recipe.RecipeName)
                {
                    order.IsBeingPrepared = false;
                    _deliveryUI.UpdateWaitingOrderList(_waitingOrders);
                    break;
                }
            }
        }
    }

    public bool HasAnyWaitingOrder()
    {
        return _waitingOrders.Count > 0;
    }

    public bool EvaluateRecipe(KitchenObject kitchenObj)
    {
        DebugCode();
        if (kitchenObj.MyRecipe.MyCompletionStatus.IsCompleted)
        {
            Debug.Log("completed meal");
            _deliveredOrderCount++;
            Destroy(kitchenObj.gameObject);

            if (_deliveredOrderCount == _totalNumberOfOrdersToPrepare)
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
    #endregion
}

public class OrderInfo
{
    public OrderInfo(Recipe recipe, bool isCompleted = false, bool isBeingPrepared = false)
    {
        MyRecipe = recipe;
        IsCompleted = isCompleted;
        IsBeingPrepared = isBeingPrepared;
    }

    public Recipe MyRecipe;
    public bool IsCompleted;
    public bool IsBeingPrepared;
}