using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graber : MonoBehaviour
{
    private Rotator _rotator;
    private Monster _monster;
    private bool _grabed;
    private IMonsterHolder _monsterHolder;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Grab();

        if (Input.GetMouseButtonUp(0))
            Release();

        if (_grabed)
            _monster.transform.position = GetWorldPositionOnPlane(Input.mousePosition, 0.5f);
    }

    private void Grab()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] raycastHits = Physics.RaycastAll(ray, 50f);

        foreach (var hitInfo in raycastHits)
        {
            if (hitInfo.collider.TryGetComponent(out IMonsterHolder monsterHolder))
            {
                if (monsterHolder.Grab(out Monster monster))
                {
                    _monster = monster;
                    _grabed = true;
                    _monsterHolder = monsterHolder;

                    _rotator = monster.GetComponentInChildren<Rotator>();
                    _rotator.enabled = true;
                }
            }
        }
    }

    private void Release()
    {
        if (_grabed == false)
            return;

        _grabed = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] raycastHits = Physics.RaycastAll(ray, 50f);

        foreach (var hitInfo in raycastHits)
        {
            if (hitInfo.collider.TryGetComponent(out IMonsterHolder monsterHolder))
            {
                if (monsterHolder.TryAcquireMonster(_monster) == false)
                    Return(_monsterHolder);

                return;
            }
        }

        Return(_monsterHolder);
        ResetPosition();
    }

    private void Return(IMonsterHolder monsterHolder)
    {
        monsterHolder.TryAcquireMonster(_monster);
        _rotator.enabled = false;
    }

    private void ResetPosition()
    {
        _monster.transform.localPosition = Vector3.zero;
    }

    private Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float yPostion)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xzPlane = new Plane(Vector3.up, new Vector3(0, yPostion, 0));
        float distance;
        xzPlane.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
