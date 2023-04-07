using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderUI : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI _orderName;
    [SerializeField] UnityEngine.UI.Image _bgImg;
    [SerializeField] Color _preparingColor = Color.green;

    public void SetOrderUI(string orderName, bool isBeingPrepared)
    {
        _orderName.text = orderName;
        if(isBeingPrepared)
        {
            _bgImg.color = _preparingColor;
        }
    }

}
