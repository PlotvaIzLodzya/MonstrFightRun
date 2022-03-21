using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterHandlerColliders : MonoBehaviour
{
    private List<BoxCollider> _boxColliders = new List<BoxCollider>();

    public void CreateBoxCollider(MonsterPlace monsterPlace)
    {
        var boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        boxCollider.center = monsterPlace.transform.localPosition;
        boxCollider.center = new Vector3(boxCollider.center.x, 0.5f, boxCollider.center.z);
        _boxColliders.Add(boxCollider);
    }
}
