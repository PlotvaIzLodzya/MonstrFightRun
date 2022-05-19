using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonsterCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _counterView;
    [SerializeField] private ParticleSystem _particleSystem;

    private MonsterPlaceAccepter[] _monsterPlaceAccepters;
    private TextAnimation _textAnimation = new TextAnimation();

    public int MaxCount { get; private set; }
    public int CurrentCount { get; private set; }

    private void OnEnable()
    {
        _monsterPlaceAccepters = FindObjectsOfType<MonsterPlaceAccepter>();

        foreach (var monsterPlaceAccepter in _monsterPlaceAccepters)
        {
            monsterPlaceAccepter.Changed += OnChangded;
        }
    }

    private void OnDisable()
    {
        foreach (var monsterPlaceAccepter in _monsterPlaceAccepters)
        {
            monsterPlaceAccepter.Changed -= OnChangded;
        }
    }

    private void OnChangded()
    {
        CurrentCount = 0;
        MaxCount = 0;

        foreach (var monsterPlaceAccepter in _monsterPlaceAccepters)
        {
            if (monsterPlaceAccepter.IsFree == false)
                CurrentCount++;

            if (monsterPlaceAccepter.IsOpened)
                MaxCount++;
        }

        _counterView.text = $"{CurrentCount}/{MaxCount}";

        if (CurrentCount < MaxCount)
        {
            _particleSystem.Stop();
            _counterView.color = Color.white;

            return;
        }

        _particleSystem.Play();
        StartCoroutine(_textAnimation.WoopAnimation(_counterView.rectTransform, 1.7f));
        _counterView.color = Color.green;

    }
}
