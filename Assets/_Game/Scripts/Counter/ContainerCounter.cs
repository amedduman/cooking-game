using System;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

public class ContainerCounter : Counter
{
    [SerializeField] KitchenObjectSO _kitchenObjectSO;
    [SerializeField] SpriteRenderer _iconSpriteRenderer;
    [GetComponentInChildren()] [SerializeField] Animator _animator;
    
    public override void Interact()
    {
        if (_player.MyKitchenObject != null) return;
        _animator.SetTrigger("OpenClose");
        var kitchenObject = Instantiate(_kitchenObjectSO.Prefab, _kitchenObjectPoint.position, Quaternion.identity);
        _player.PickKitchenObject(kitchenObject);
    }

    void OnValidate()
    {
        if (_iconSpriteRenderer == null || _kitchenObjectSO == null) return;
        _iconSpriteRenderer.sprite = _kitchenObjectSO.Icon;
    }
}