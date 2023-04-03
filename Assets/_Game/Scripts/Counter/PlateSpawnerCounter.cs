using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateSpawnerCounter : Counter
{
    [SerializeField] KitchenObject _platePrefab;
    [SerializeField] int _maxPlateCount = 4;
    [SerializeField] float _plateSpawnInterval = 4;
    [SerializeField] float _plateThickness = 1;
    float _plateSpawnTimer;
    Coroutine _plateSpawnCoroutine;
    Stack<KitchenObject> _plates = new Stack<KitchenObject>();

    private void Start()
    {
        _plateSpawnCoroutine =  StartCoroutine(SpawnPlate());
    }

    IEnumerator SpawnPlate()
    {
        while (true)
        {
            yield return null;

            _plateSpawnTimer += Time.deltaTime;
            if (_plateSpawnTimer > _plateSpawnInterval)
            {
                if (_plates.Count < _maxPlateCount)
                {
                    var plate = Instantiate(_platePrefab, _kitchenObjectPoint.position, Quaternion.identity, transform);
                    _plates.Push(plate);
                    var pos = _kitchenObjectPoint.position;
                    pos.y += _plateThickness * _plates.Count;
                    plate.transform.position = pos;
                    _plateSpawnTimer = 0;
                }
            }
        }
    }

    public override void Interact()
    {
        if (_plates.Count < 1) return;
        if (_player.MyKitchenObject != null) return;
        var plate = _plates.Pop();
        plate.InstantiateRecipe();
        _player.PickKitchenObject(plate);
    }
}
