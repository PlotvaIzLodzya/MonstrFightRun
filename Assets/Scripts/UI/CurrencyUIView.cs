using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyUIView : MonoBehaviour
{
    [SerializeField] private TMP_Text _currencyUI;
    [SerializeField] private WalletView _walletView;

    private void OnEnable()
    {
        StartCoroutine(Animation(_currencyUI, (int)_walletView.CurrencyCollected));
    }

    private IEnumerator Animation(TMP_Text text, int value)
    {
        int textValue = 0;
        float delay = 0;
        text.text = $"{(int)textValue}";

        if(value>0)
            delay = (2f / (float)value);

        while (textValue< value)
        {
            yield return new WaitForSeconds(delay);

            textValue ++;
            text.text = $"{(int)textValue}";
        }
    }
}
