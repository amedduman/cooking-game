using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _selectedCounterVisual;
    Coroutine _highlightCoroutine;
    
    public virtual void Interact()
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
    
}
