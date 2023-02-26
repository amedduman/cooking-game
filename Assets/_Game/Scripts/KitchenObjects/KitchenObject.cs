using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class KitchenObject : MonoBehaviour
{
    [field:SerializeField] public bool IsSliceable { get; private set; }
    [field:SerializeField] public int NecessaryHitToSlice { get; private set; } = 4;

    public int CurrentHitCount { get; private set; }
    

    public bool IsSliced { get; private set; }
    [SerializeField] GameObject _normalVisual;
    [SerializeField] GameObject _slicedVisual;
    
    public void Slice()
    {
        if (IsSliced) return;

        CurrentHitCount++;

        if (CurrentHitCount >= NecessaryHitToSlice)
        {
            _normalVisual.SetActive(false);
            _slicedVisual.SetActive(true);
            IsSliced = true;
        }
    }
}
