using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class ProgressBar : MonoBehaviour
{
    [SerializeField] Image _barImg;
    CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        _canvasGroup.alpha = 1;
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
    }

    public void UpdateProgressBar(float fillAmount)
    {
        _barImg.DOFillAmount(fillAmount, .1f);   
    }
}
