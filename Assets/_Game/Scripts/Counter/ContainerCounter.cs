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
        _animator.SetTrigger("OpenClose");
        var kitchenObject = Instantiate(_kitchenObjectSO.Prefab, _kitchenObjectPoint.position, Quaternion.identity);
        _player.PickKitchenObject(kitchenObject);
    }

    public override bool IsCounterAvailableToInteract(Player player)
    {
        // don't allow player to interact with container counter if player has an item in its hands
        if (player.MyKitchenObject != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void OnValidate()
    {
        if (_iconSpriteRenderer == null || _kitchenObjectSO == null) return;
        _iconSpriteRenderer.sprite = _kitchenObjectSO.Icon;
    }
}