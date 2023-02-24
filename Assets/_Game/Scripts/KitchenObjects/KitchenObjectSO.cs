using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CookingGame/KitchenObject")]
public class KitchenObjectSO : ScriptableObject
{
    [field: SerializeField] public KitchenObject Prefab { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
}
