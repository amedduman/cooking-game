using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    [SerializeField] RecepieVisualInfo[] RecepieVisualInfoList;

    public bool TryToAddIngredient(KitchenObject kitchenObj)
    {
        if (kitchenObj.IsIngredient == false) return false;
        //foreach (var recepieVisualInfo in RecepieVisualInfoList)
        //{
        //    if (kitchenObj.MyKitchenObjSo == recepieVisualInfo.MyKitchenObjSO)
        //    {
        //        Debug.Log(recepieVisualInfo.IsAdded);
        //        if (recepieVisualInfo.IsAdded) continue;
        //        recepieVisualInfo.Visual.SetActive(true);
        //        recepieVisualInfo.SetIsAdded(true);
        //        return true;
        //    }
        //}
        for (int i = 0; i < RecepieVisualInfoList.Length; i++)
        {
            if (kitchenObj.MyKitchenObjSo == RecepieVisualInfoList[i].MyKitchenObjSO)
            {
                if (RecepieVisualInfoList[i].IsAdded == false)
                {
                    RecepieVisualInfoList[i].Visual.SetActive(true);
                    RecepieVisualInfoList[i].IsAdded = true;
                    return true;
                }
            }
        }
        return false;
    }
}


[System.Serializable]
struct RecepieVisualInfo
{
    public KitchenObjectSO MyKitchenObjSO;
    public GameObject Visual;
    public bool IsAdded;

    public void SetIsAdded(bool val)
    {
        IsAdded = val;
    }
}
