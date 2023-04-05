using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderUI : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI _orderName;

    public void SetOrderName(string orderName)
    {
        _orderName.text = orderName;
    }

}
