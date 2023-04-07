using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderUI : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI _orderName;
    [SerializeField] UnityEngine.UI.Image _bgImg;

    public void SetOrderUI(string orderName, bool isBeingPrepared)
    {
        _orderName.text = orderName;
        if(isBeingPrepared)
        {
            _bgImg.color = Color.green;
        }
        else
        {
            _bgImg.color = Color.red;
        }
    }

}
