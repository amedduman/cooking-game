using System;
using UnityEngine;

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
        Cooking,
        Cooked,
        Burned,
    }
    
    [field:SerializeField] public bool IsCookable { get; private set; }
    
    [field: SerializeField] public float TimeToCook { get; private set; } = 3;
    [field: SerializeField] public float TimeToBurn { get; private set; } = 3;
    public cookingState MyCookingState { get; set; } = cookingState.Raw;
    public GameObject RawVisual;
    public GameObject CookedVisual;
    public GameObject BurnedVisual;
    public float CurentTimeOnStove = 0;

    public void ChangeVisual()
    {
        switch (MyCookingState)
        {
            case cookingState.Raw:
                CookedVisual.gameObject.SetActive(false);
                BurnedVisual.gameObject.SetActive(false);
                break;
            case cookingState.Cooking:
                CookedVisual.gameObject.SetActive(false);
                BurnedVisual.gameObject.SetActive(false);
                break;
            case cookingState.Cooked:
                RawVisual.gameObject.SetActive(false);
                CookedVisual.gameObject.SetActive(true);
                BurnedVisual.gameObject.SetActive(false);
                break;
            case cookingState.Burned:
                RawVisual.gameObject.SetActive(false);
                CookedVisual.gameObject.SetActive(false);
                BurnedVisual.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    #endregion
}
