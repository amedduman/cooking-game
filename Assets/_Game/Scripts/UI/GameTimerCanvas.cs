using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimerCanvas : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI _timerTxt;

    public void UpdateTheTimer(int timer)
    {
        _timerTxt.text = timer.ToString();
    }
}
