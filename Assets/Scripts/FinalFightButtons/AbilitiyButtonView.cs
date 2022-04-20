using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class AbilitiyButtonView : Ability, IPointerClickHandler
{
    [SerializeField] private TMP_Text _amount;
    [SerializeField] private TMP_Text _value;
    [SerializeField] private Image _cooldownImage;

    private void Start()
    {
        UpdateView();
    }

    private void OnEnable()
    {
        ValueHandler.AmountChanged += UpdateView;
        AmountHandler.AmountChanged += UpdateView;
        UpdateView();
    }

    private void OnDisable()
    {
        ValueHandler.AmountChanged -= UpdateView;
        AmountHandler.AmountChanged -= UpdateView;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Use();
        UpdateView();
    }

    protected override void OnAbilityCasted()
    {
        StartCoroutine(CooldownAnimation());
    }

    private void UpdateView()
    {
        _amount.text = $"{AmountHandler.Amount }";
        _value.text = $"{ValueHandler.Amount}";
    }

    private IEnumerator CooldownAnimation()
    {
        while (IsOnCooldown)
        {
            _cooldownImage.fillAmount = ElapsedTime;

            yield return null;
        }
    }
}
