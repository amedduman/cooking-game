using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Counter : MonoBehaviour, IInteractable
{
    protected Player _player;
    [SerializeField] GameObject _selectedCounterVisual;
    Coroutine _highlightCoroutine;
    [SerializeField] protected Transform _kitchenObjectPoint;

    public void Awake()
    {
        _player = ServiceLocator.Get<Player>();
    }

    public abstract void Interact();

    public virtual void InteractAlternate()
    {
        
    }

    public void Highlight()
    {
        if(_highlightCoroutine != null)
            StopCoroutine(_highlightCoroutine);
        _selectedCounterVisual.SetActive(true);
        _highlightCoroutine = StartCoroutine(DisableHighlight());
    }

    IEnumerator DisableHighlight()
    {
        yield return new WaitForEndOfFrame();
        _selectedCounterVisual.SetActive(false);
    }

    protected void PutKitchenObjToPos(KitchenObject myKitchenObj)
    {
        myKitchenObj.transform.parent = _kitchenObjectPoint;
        myKitchenObj.transform.DOLocalMove(Vector3.zero, .2f);
    }
}
