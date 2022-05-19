using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class Graber : MonoBehaviour
{
    private Rotator _rotator;
    private Monster _monster;
    private bool _grabed;
    private IMonsterHolder _monsterHolder;
    private float _pointerDistance;
    private float _threshold =1f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _pointerDistance = 0;
        }

        if (Input.GetMouseButton(0))
        {
            if (IsBrakingThreshold())
                Grab();

            _pointerDistance += Mathf.Abs(Input.GetAxis("Mouse X")) + Mathf.Abs(Input.GetAxis("Mouse Y"));
        }

        if (Input.GetMouseButtonUp(0))
        {
            TryOpenInfoPanel();

            Release();
        }

        if (_grabed)
            _monster.transform.position = GetWorldPositionOnPlane(Input.mousePosition, 2f);
    }

    private void Grab()
    {
        if (_grabed == true)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] raycastHits = Physics.RaycastAll(ray, 50f);

        foreach (var hitInfo in raycastHits)
        {
            if (hitInfo.collider.TryGetComponent(out IMonsterHolder monsterHolder))
            {
                if (monsterHolder.TryGrab(out Monster monster))
                {
                    _monster = monster;
                    _grabed = true;
                    _monsterHolder = monsterHolder;
                    _monster.MonsterAnimator.VictoryAnimation();

                    _rotator = monster.GetComponentInChildren<Rotator>();
                    _rotator.enabled = true;

                    LightUp(monster);
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
                {
                    if (monsterHolder is MonsterPlaceAccepter)
                        Swap((MonsterPlaceAccepter)monsterHolder);
                    else
                        Return(_monsterHolder);
                }

                _monster.MonsterAnimator.MonsterPlaced();
                LighthDown();
                return;
            }
        }

        PlaceToInitialMonsterCell(_monster);
        ResetPosition();
        LighthDown();
        _monster.MonsterAnimator.MonsterPlaced();
    }

    private bool TryOpenInfoPanel()
    {
        if (_grabed)
            return false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] raycastHits = Physics.RaycastAll(ray, 50f);

        foreach (var hitInfo in raycastHits)
        {
            if (hitInfo.collider.TryGetComponent(out MonsterCell monsterHolder) && SwipeZone.IsMoving == false && EventSystem.current.IsPointerOverGameObject() == false)
            {
                monsterHolder.TryOpenInfoPanel();

                return true;
            }
        }

        return false;
    }

    private void LightUp(Monster monster)
    {

        var cells = FindObjectsOfType<MonsterCell>();

        foreach (var cell in cells)
        {
            if(cell.InitialMonster.GetType() == monster.GetType())
                cell.LightUp();
        }
    }

    private void LighthDown()
    {

        var cells = FindObjectsOfType<MonsterCell>();

        foreach (var cell in cells)
        {
            cell.LightDown();
        }
    }

    private bool TryGetMonsterHolder(IMonsterHolder monsterHolder, out IMonsterHolder targetMonsterHolder)
    {
        targetMonsterHolder = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] raycastHits = Physics.RaycastAll(ray, 50f);

        foreach (var hitInfo in raycastHits)
        {
            if (hitInfo.collider.TryGetComponent(out IMonsterHolder tempMonsterHolder))
                if (tempMonsterHolder.GetType() == monsterHolder.GetType())
                    targetMonsterHolder = tempMonsterHolder;
        }

        return false;
    }

    private void Swap(MonsterPlaceAccepter monsterPlaceAccepter)
    {
        monsterPlaceAccepter.TryGrab(out Monster firstMonster);

        monsterPlaceAccepter.TryAcquireMonster(_monster);

        TryPlaceToInititalMonsterCell(firstMonster);  
    }

    private bool TryPlaceToInititalMonsterCell(Monster monster)
    {
        if (_monsterHolder.TryAcquireMonster(monster) == false)
        {
            monster.gameObject.SetActive(false);
            PlaceToInitialMonsterCell(monster);
            return false;
        }

        return true;
    }

    private void PlaceToInitialMonsterCell(Monster monster)
    {
        MonsterCell[] monsterCells = FindObjectsOfType<MonsterCell>();

        foreach (var monsterCell in monsterCells)
        {
            if (monsterCell.TryAcquireMonster(monster))
                return;
        }
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

    private bool IsBrakingThreshold()
    {
        return _pointerDistance > _threshold;
    }
}
