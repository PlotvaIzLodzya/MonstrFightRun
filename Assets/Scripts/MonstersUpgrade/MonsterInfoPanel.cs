using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MonsterInfoPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _lvlText;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _attackText;
    [SerializeField] private AddToPartyButton _addToPartyButton;
    [SerializeField] private Image _attackRangeImage;
    [SerializeField] private MonstersIcons _monstersIcons;

    private Monster _monster;

    public void Open(MonsterCell monsterCell)
    {
        _monster = monsterCell.Monster;
        gameObject.SetActive(true);
        _addToPartyButton.Init(monsterCell);

        UpdateInfo();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void UpdateInfo()
    {
        _lvlText.text = $"Lv. {_monster.Level}";
        _nameText.text = $"{_monster.Name}";
        _healthText.text = $"{_monster.Health.MaxHealth}";
        _attackText.text = $"{_monster.Damage}";
        _attackRangeImage.sprite = _monstersIcons.GetAttackRangeIconSprite(_monster.Attack.InitialRange);
    }
}
