using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DailyRewardPresenter : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Animator _animator;
    [SerializeField] private DailyRewardBehaviour _dailyRewardBehaviour;

    private void Awake()
    {
        _dailyRewardBehaviour.UpdateInfo();
    }

    private const string Pressed = "Pressed";

    public void OnPointerDown(PointerEventData eventData)
    {
        _animator.SetTrigger(Pressed);
        _dailyRewardBehaviour.Acquire();
    }
}
