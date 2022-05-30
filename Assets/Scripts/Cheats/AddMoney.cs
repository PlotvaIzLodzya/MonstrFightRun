using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddMoney : MonoBehaviour
{
    [SerializeField] private Button _button;

    private Player _player;
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _player.CurrencyHandler.Increase(200);
    }
}
