using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour, IInteractable
{
    public virtual void Interact()
    {
        Debug.Log("interact");
    }
}
