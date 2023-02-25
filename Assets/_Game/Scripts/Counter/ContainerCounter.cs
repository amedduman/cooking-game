using UnityEngine;

public class ContainerCounter : Counter
{
    [SerializeField] KitchenObjectSO _kitchenObjectSO;
    [SerializeField] Vector3 _spawningPoint;
    
    public override void Interact()
    {
        var kitchenObject = Instantiate(_kitchenObjectSO.Prefab, GetSpawnPoint(), Quaternion.identity);
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

    Vector3 GetSpawnPoint()
    {
        return transform.position + _spawningPoint;
    }
    
    void OnDrawGizmosSelected()
    {
        //draw spawn point
        Gizmos.DrawSphere(GetSpawnPoint(), .1f);
    }
}