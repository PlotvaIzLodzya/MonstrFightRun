using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _currencyAmount;
    [SerializeField] private Image _image;
    [SerializeField] private FlyingPicture _flyingPicture;

    private Player _player;

    public Image Image => _image;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        Error.CheckOnNull(_player, nameof(Player));
    }

    private void OnEnable()
    {
        _player.AmountHandler.AmountIncreased += OnAmountChange;
        _player.AmountHandler.AmountLoaded += OnAmountLoaded;
        _player.AmountHandler.AmountDecreased += ChangeViewText;
    }

    private void OnDisable()
    {
        _player.AmountHandler.AmountIncreased -= OnAmountChange;
        _player.AmountHandler.AmountLoaded -= OnAmountLoaded;
        _player.AmountHandler.AmountDecreased -= ChangeViewText;
    }

    public void ChangeViewText(float amount)
    {
        _currencyAmount.text = $"{amount}";
    }

    private void OnAmountChange(float amount)
    {
        var flyingPicture = Instantiate(_flyingPicture, transform);
        flyingPicture.transform.position = _player.UiHandler.Position;
        flyingPicture.Init(_image.transform.localPosition, _image.sprite, _image.rectTransform.rect.width, this, amount);
    }

    private void OnAmountLoaded(float amount)
    {
        ChangeViewText(amount);
        _player.AmountHandler.AmountLoaded -= OnAmountLoaded;
    }
}
