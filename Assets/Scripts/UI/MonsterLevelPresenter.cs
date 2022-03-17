using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonsterLevelPresenter : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private Monster _monster;

    private float _maxFontSize;
    private float _minFontSize;

    private void Awake()
    {
        _minFontSize = _level.fontSize;
        _maxFontSize = _minFontSize + 20f;
    }

    private void OnEnable()
    {
        _monster.LevelChanged += OnMerge;
    }

    private void OnDisable()
    {
        _monster.LevelChanged -= OnMerge;
    }

    public void OnMerge(int level)
    {
        if(_canvas.gameObject.activeInHierarchy == false)
            _canvas.gameObject.SetActive(true);

        _level.text = $"{level}";
        StartCoroutine(Animation(_level));
    }

    private IEnumerator Animation(TMP_Text text)
    {
        float changeSpeed = (_maxFontSize - _minFontSize) / 0.1f;

        while (text.fontSize < _maxFontSize)
        {
            text.fontSize = Mathf.MoveTowards(text.fontSize, _maxFontSize, changeSpeed * Time.deltaTime);
            yield return null;
        }

        while (text.fontSize > _minFontSize)
        {
            text.fontSize = Mathf.MoveTowards(text.fontSize, _minFontSize, changeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
