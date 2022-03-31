using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerMovementSystem.Examples;
using RunnerMovementSystem;

public class CreoController : MonoBehaviour
{
    [SerializeField] private float _delay;
    [Space(15)]
    [SerializeField] private MovementSystem  _cameraCarriageMovementSystem;
    [SerializeField] private CameraFollowing _cameraFollowing;
    [Space(15)]
    [SerializeField] private MovementSystem _playerMovementSystem;
    [SerializeField] private MouseInput _mouseInput;
    [Space(15)]
    [SerializeField] private MovementSystem _targetMovementSystem;
    [SerializeField] private MouseInput _TargetmouseInput;

    private Boss[] _bosses;

    private void Awake()
    {
        _bosses = FindObjectsOfType<Boss>();
        StartCoroutine(HideHP());


        _cameraCarriageMovementSystem.enabled = false;
        _cameraFollowing.enabled = false;
        StartCoroutine(StartDelay());
    }

    private IEnumerator HideHP()
    {
        foreach (var boss in _bosses)
        {
            boss.GetComponent<UIHandler>().SwitchState();
        }

        yield return new WaitForSeconds(5f);

        foreach (var boss in _bosses)
        {
            boss.GetComponent<UIHandler>().SwitchState();
        }
    }
    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(_delay);

        _cameraCarriageMovementSystem.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CameraFollowingTrigger cameraFollowingTrigger))
        {
            _cameraFollowing.enabled = true;
            _cameraFollowing.transform.parent = null;
        }

        if(other.TryGetComponent(out StartMovementTrigger startMovementTrigger))
        {
            _mouseInput.StartRun();
            _TargetmouseInput.StartRun();
        }    
    }
}
