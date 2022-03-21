using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Monster))]
public class MonsterLevelPresenter : LevelPresenter
{
    [SerializeField] private Canvas _canvas;

    private Monster _monster;
    private float _maxFontSize;
    private float _minFontSize;

    private void Awake()
    {
        _monster = GetComponent<Monster>();
        _minFontSize = Level.fontSize;
        _maxFontSize = _minFontSize + 20f;
    }

    private void OnEnable()
    {
        _monster.LevelChanged += OnLevelChange;
        Show(_monster.Level);
    }

    private void OnDisable()
    {
        _monster.LevelChanged -= OnLevelChange;
    }

    public void Disable()
    {
        _canvas.gameObject.SetActive(false);
    }

    public void OnLevelChange(int level)
    {
        if(_canvas.gameObject.activeInHierarchy == false)
            _canvas.gameObject.SetActive(true);

        Show(level);
        StartCoroutine(Animation(Level));
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
