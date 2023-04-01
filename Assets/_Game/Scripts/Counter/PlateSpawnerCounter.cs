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
    int _plateCount;

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
                if (_plateCount < _maxPlateCount)
                {
                    Instantiate(_platePrefab, _kitchenObjectPoint.position, Quaternion.identity, transform);
                    _plateCount++;
                    var pos = _kitchenObjectPoint.position;
                    pos.y += _plateThickness;
                    _kitchenObjectPoint.position = pos;
                    _plateSpawnTimer = 0;
                }
            }
        }
    }

    public override void Interact()
    {
        
    }
}
