using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    [SerializeField] RecepieVisualInfo[] RecepieVisualInfoList;

    public void TryToAddIngredient(KitchenObject kitchenObj)
    {
        
        if (kitchenObj.IsIngredient == false) return;
        foreach (var recepieVisualInfo in RecepieVisualInfoList)
        {
            //if (kitchenObj.MyKitchenObjSo.GetInstanceID() == recepieVisualInfo.MyKitchenObjSO.GetInstanceID())
            //{
            //    recepieVisualInfo.Visual.SetActive(true);
            //}
            if (kitchenObj.MyKitchenObjSo == recepieVisualInfo.MyKitchenObjSO)
            {
                recepieVisualInfo.Visual.SetActive(true);
            }
        }
    }
}


[System.Serializable]
struct RecepieVisualInfo
{
    public KitchenObjectSO MyKitchenObjSO;
    public GameObject Visual;
}
