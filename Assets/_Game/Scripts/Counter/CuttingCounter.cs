using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CuttingCounter : Counter
{
    [SerializeField] Animator _animator;
    [SerializeField] Image _progressBar;
    
    KitchenObject _myKitchenObj;

    void Start()
    {
        _progressBar.fillAmount = 0;
        _progressBar.GetComponentInParent<Canvas>().gameObject.SetActive(false);
    }

    public override void Interact()
    {
        if (_myKitchenObj == null)
        {
            if (_player.MyKitchenObject == null) return;
            if (!_player.MyKitchenObject.IsSliceable) return;
            if (_player.MyKitchenObject.IsSliced) return;

            var takenKitchenObj = _player.DropKitchenObject();
            _myKitchenObj = takenKitchenObj;
            PutKitchenObjToPos();
            
            return;
        }
        
        if (_myKitchenObj.IsSliced)
        {
            if (_player.MyKitchenObject != null)
            {
                if (_player.MyKitchenObject.IsSliced) return;
                if (!_player.MyKitchenObject.IsSliceable) return;
                
                var newKitchenObj = _player.MyKitchenObject;
                _player.PickKitchenObject(_myKitchenObj);
                _myKitchenObj = newKitchenObj;
                PutKitchenObjToPos();
            }
            else
            {
                _player.PickKitchenObject(_myKitchenObj);
                _myKitchenObj = null;
            }
                
        }
        else
        {
            if (_myKitchenObj.CurrentHitCount == 0)
            {
                if (_player.MyKitchenObject == null)
                {
                    _player.PickKitchenObject(_myKitchenObj);
                    _myKitchenObj = null;
                }
                else
                {
                    if (_player.MyKitchenObject.IsSliceable)
                    {
                        var newKitchenObj = _player.MyKitchenObject;
                        _player.PickKitchenObject(_myKitchenObj);
                        _myKitchenObj = newKitchenObj;
                        PutKitchenObjToPos();
                    }
                }
            }
        }
    }

    public override void InteractAlternate()
    {
        if (_myKitchenObj == null) return;
        
        if (_myKitchenObj.IsSliceable)
        {
            _myKitchenObj.Slice();
            _animator.SetTrigger("Cut");
            UpdateProgressBar();
        }
        else
        {
            Debug.LogError("system shouldn't allow call slice function on non-sliceable kitchen objects. because system shouldn't allow put non slice-able object in the cutting counter");
        }
    }
    
    void UpdateProgressBar()
    {
        float amount = (float)_myKitchenObj.CurrentHitCount / _myKitchenObj.NecessaryHitToSlice;
        if(!Mathf.Approximately(amount, 0))
            _progressBar.transform.parent.gameObject.SetActive(true);
        _progressBar.DOFillAmount(amount, .1f);
        if(Mathf.Approximately(amount, 1))
            _progressBar.transform.parent.gameObject.SetActive(false);
    }

    void PutKitchenObjToPos()
    {
        _myKitchenObj.transform.parent = _kitchenObjectPoint;
        _myKitchenObj.transform.DOLocalMove(Vector3.zero, .2f);
    }
}
