using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class DeliveryCounterUI : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI _resultTxt;
    [SerializeField] UnityEngine.UI.Image _bgImg;

    [SerializeField] string _successText = "succsessful delivery";
    [SerializeField] string _faultyText = "faulty delivery";
    [SerializeField] string _notCompletedText = "not completed";
    [SerializeField] Color _successColor = Color.green;
    [SerializeField] Color _faultyColor = Color.green;
    [SerializeField] Color _notCompletedColor = Color.green;

    CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
    }

    public void ShowDeliveryResultUI(Recipe recipe)
    {
        if(recipe.MyCompletionStatus.IsCompleted)
        {
            if (recipe.MyCompletionStatus.IsFaulty)
            {
                ShowUI(_faultyText, _faultyColor);
            }
            else
            {
                ShowUI(_successText, _successColor);
            }
        }
        else
        {
            ShowUI(_notCompletedText, _notCompletedColor);
        }
        

        void ShowUI(string text, Color color)
        {
            _canvasGroup.alpha = 1;
            transform.DOShakeScale(.1f, .1f);
            _resultTxt.text = text;
            _bgImg.color = color;
            DOVirtual.DelayedCall(.8f, () =>
            {
                _canvasGroup.alpha = 0;
            });
        }
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }
}
