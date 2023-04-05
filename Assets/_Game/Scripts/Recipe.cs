using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    public String RecipeName;
    public CompletionStatus MyCompletionStatus;
    [SerializeField] RecepieVisualInfo[] RecepieVisualInfoList;

    public bool TryToAddIngredient(KitchenObject kitchenObj)
    {

        if (kitchenObj.IsIngredient == false) return false;

        for (int i = 0; i < RecepieVisualInfoList.Length; i++)
        {
            if (kitchenObj.MyKitchenObjSo == RecepieVisualInfoList[i].MyKitchenObjSO)
            {
                if (RecepieVisualInfoList[i].IsAdded == false)
                {
                    RecepieVisualInfoList[i].Visual.SetActive(true);
                    RecepieVisualInfoList[i].IconBg.color = Color.green;
                    RecepieVisualInfoList[i].IsAdded = true;
                    return true;
                }
            }

            if(kitchenObj.MyKitchenObjSo == RecepieVisualInfoList[i].FailedVersion)
            {
                RecepieVisualInfoList[i].FailedVersionVisual.SetActive(true);
                RecepieVisualInfoList[i].Icon.gameObject.SetActive(false);
                RecepieVisualInfoList[i].FailedVersionIcon.gameObject.SetActive(true);
                RecepieVisualInfoList[i].IconBg.color = Color.red;
                RecepieVisualInfoList[i].IsAdded = true;
                MyCompletionStatus.IsFaulty = true;
                return true;
            }
        }

        CheckCompletion();

        return false;
    }

    void CheckCompletion()
    {
        var completion = true;
        for (int i = 0; i < RecepieVisualInfoList.Length; i++)
        {
            if(RecepieVisualInfoList[i].IsAdded == false)
            {
                completion = false;
            }
        }

        MyCompletionStatus.IsCompleted = completion;
    }

    void UpdateUI()
    {
            
    }
}


[System.Serializable]
struct RecepieVisualInfo
{
    public KitchenObjectSO MyKitchenObjSO;
    public GameObject Visual;
    public Image Icon;
    public Image IconBg;
    [HideInInspector] public bool IsAdded;
    public bool CanBeFailed;
    [ShowIf(nameof(CanBeFailed), true)] public KitchenObjectSO FailedVersion;
    [ShowIf(nameof(CanBeFailed), true)] public GameObject FailedVersionVisual;
    [ShowIf(nameof(CanBeFailed), true)] public Image FailedVersionIcon;  

}

public struct CompletionStatus
{
    public bool IsCompleted;
    public bool IsFaulty;
}

