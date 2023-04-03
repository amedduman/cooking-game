using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : Counter
{
    KitchenObject _myKitchenObj;
    Coroutine _cookingTimerCoroutine;
    [SerializeField] ProgressBar _progressBar;

    void Start()
    {
        _progressBar.UpdateProgressBar(0);
        _progressBar.Hide();
    }

    public override void Interact()
    {
        if (_myKitchenObj == null)
        {
            if (_player.MyKitchenObject == null) return;
            if (_player.MyKitchenObject.IsCookable == false) return;
            if (_player.MyKitchenObject.MyCookingState != KitchenObject.cookingState.Raw) return;

            var takenKitchenObj = _player.DropKitchenObject();
            _myKitchenObj = takenKitchenObj;
            PutKitchenObjToPos(_myKitchenObj);
            PutOnStove();
            return;
        }
        else
        {
            if (_player.MyKitchenObject != null) return; // TODO : improve this. we want to be able to change items if player's current holdign item is cookable/raw and sotve's current item is not cooking
            if (_myKitchenObj.MyCookingState == KitchenObject.cookingState.Cooking) return;
            RemoveFromStove();
        }
    }

    public void PutOnStove()
    {
        _myKitchenObj.MyCookingState = KitchenObject.cookingState.Cooking;
        _cookingTimerCoroutine = StartCoroutine(CookingTimer());
    }

    public void RemoveFromStove()
    {
        StopCoroutine(_cookingTimerCoroutine);
        _player.PickKitchenObject(_myKitchenObj);
        _myKitchenObj = null;
    }

    IEnumerator CookingTimer()
    {
        while (true)
        {
            _myKitchenObj.CurrentTimeOnStove += Time.deltaTime;
            yield return null;
            UpdateProgressBar();
            if(_myKitchenObj.CurrentTimeOnStove > _myKitchenObj.TimeToCook)
            {
                _myKitchenObj.MyCookingState = KitchenObject.cookingState.Cooked;
                _myKitchenObj.ChangeVisual();
            }
            if(_myKitchenObj.CurrentTimeOnStove > _myKitchenObj.TimeToCook + _myKitchenObj.TimeToBurn)
            {
                _myKitchenObj.MyCookingState = KitchenObject.cookingState.Burned;
                _myKitchenObj.ChangeVisual();
            }
        }
    }

    void UpdateProgressBar()
    {
        if (_myKitchenObj == null) return;

        float fillAmount = _myKitchenObj.CurrentTimeOnStove / _myKitchenObj.TimeToCook;
        if(fillAmount < 1)
        {
            _progressBar.Show();
            _progressBar.UpdateProgressBar(fillAmount);
        }
        else
        {
            _progressBar.Hide();
        }
    }
}
