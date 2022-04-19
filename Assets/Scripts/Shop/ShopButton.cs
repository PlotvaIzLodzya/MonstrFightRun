using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public abstract class ShopButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _cost;
    [SerializeField] private GameObject _soldOut;
    [SerializeField] private TMP_Text _label;

    private Player _player;

    private bool _isInactive;

    private void Start()
    {
        _label.text = $"{_cost}";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isInactive)
            return;

        if (_player == null)
            _player = FindObjectOfType<Player>();

        if (_player.CurrencyWallet.TrySpend(_cost))
            Buy(_cost);
    }

    public void SetInactive()
    {
        _isInactive = true;
        _soldOut.SetActive(true);
    }
    public abstract void Buy(int cost);
}
