using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PowerUp : MonoBehaviour, IUsable
{
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out MonstersHandler monstersHandler))
        {
            Use(monstersHandler);
        }

        if(other.TryGetComponent(out Monster monster))
        {
            Use(monster);
        }
    }

    public virtual void Use(MonstersHandler monstersHandler) { }

    public virtual void Use(Monster monster) { }
}
