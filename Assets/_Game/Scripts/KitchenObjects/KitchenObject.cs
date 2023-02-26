using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [field:SerializeField] public bool IsSliceable { get; private set; }
    public int CurrentHitCount { get; private set; }

    public bool IsSliced { get; private set; }
    [SerializeField] GameObject _normalVisual;
    [SerializeField] GameObject _slicedVisual;
    [SerializeField] int _necessaryHitToSlice = 4;
    

    public void Slice()
    {
        if (IsSliced) return;
        
        CurrentHitCount++;

        if (CurrentHitCount >= _necessaryHitToSlice)
        {
            _normalVisual.SetActive(false);
            _slicedVisual.SetActive(true);
            IsSliced = true;
        }
    }
}
