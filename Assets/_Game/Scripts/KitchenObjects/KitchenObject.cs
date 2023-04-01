using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class KitchenObject : MonoBehaviour
{
    #region SliceLogic

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

    #endregion

    #region CookLogic

    public enum cookingState
    {
        Raw,
        Cooked,
        Burned,
    }
    
    [field:SerializeField] public bool IsCookable { get; private set; }
    
    [field: SerializeField] public float TimeToFry { get; private set; } = 3;
    [field: SerializeField] public float TimeToBurn { get; private set; } = 3;
    public cookingState MyCookingState { get; private set; } = cookingState.Raw; 
    float _curentTimeOnStove = 0;
    public void Cook()
    {
        switch (MyCookingState)
        {
            case cookingState.Raw:
                
                break;
            case cookingState.Cooked:
                break;
            case cookingState.Burned:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    #endregion
}
