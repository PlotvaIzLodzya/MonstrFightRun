using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public abstract class ShopButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _soldOut;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private float _costPerBuy;
    [SerializeField] private PurchaseType _purchaseType;
    [SerializeField] private PurchaseName _purchaseName;
    [SerializeField] private float _minValue;
    [SerializeField] private float _minCost;

    protected Player Player;
    private const int MaxBuying = 100;
    private const string BuyCounterSaveName = "BuyCounter";
    private bool _isInactive;
    private ValueHandler BuyCounter;
    protected IntegrationMetric _integrationMetric = new IntegrationMetric();
    protected ValueHandler ValueHandler { get; private set; }
    protected ValueHandler CostHandler { get; private set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isInactive)
            return;

        if (Player == null)
            Player = FindObjectOfType<Player>();

        if (Player.CurrencyHandler.TryDecrease(CostHandler.Value))
        {
            BuyCounter.Increase(1);

            Buy(CostHandler.Value);

            CostHandler.Increase(_costPerBuy);

            UpdateInfo();

            _integrationMetric.OnSoftCurrencySpend(_purchaseType.ToString(), _purchaseName.ToString(), (int)CostHandler.Value);
        }
    }

    public abstract void Buy(float cost);

    public void SetInactive()
    {
        _isInactive = true;
        _soldOut.SetActive(true);
    }

    protected virtual void UpdateInfo()
    {
        _label.text = $"{CostHandler.Value}";
    }

    public void LoadProgression(string saveName)
    {
        BuyCounter = new ValueHandler(0, 100, $"{BuyCounterSaveName}{saveName}");
        BuyCounter.LoadAmount();

        ValueHandler = new ValueHandler(_minValue, 10000, saveName);
        ValueHandler.LoadAmount();

        CostHandler = new ValueHandler(_minCost, 10000, saveName + 1);
        CostHandler.LoadAmount();
        UpdateInfo();

        if (BuyCounter.Value >= MaxBuying)
            SetInactive();
    }
}

public enum PurchaseType
{
    improvment
}

public enum PurchaseName
{
    LvlImprovment,
    MonsterLevelUpgrade,
    MonsterBuying,
    CurrencyMultiplier,
    HealUpgrade,
    MeteorUpgrade,
    ProtectionUpgrade
}
