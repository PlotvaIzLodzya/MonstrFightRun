using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] private float _coolDown;
    [SerializeField] private string _abilityName;
    [SerializeField] private int _minValue;
    [SerializeField] private Sprite _icon;

    private ValueHandler _valueHandler;
    private ValueHandler _amountHandler;
    private float _elapsedTime = 1;

    public Sprite Icon => _icon;
    public string AbilityName => _abilityName;
    public bool IsOnCooldown { get; private set; }
    public ValueHandler ValueHandler => _valueHandler;
    public ValueHandler AmountHandler => _amountHandler;
    public float ElapsedTime => _elapsedTime;

    private void Awake()
    {
        _valueHandler = new ValueHandler(_minValue, _abilityName);
        _amountHandler = new ValueHandler(0, _abilityName+1);
        AmountHandler.LoadAmount();
        ValueHandler.LoadAmount();
    }

    public virtual void Cast() { }

    public void Use()
    {
        if (IsOnCooldown == false)
        {
            if (_amountHandler.TryDecrease(1))
            {
                StartCoroutine(Cooldown());
                Cast();
                OnAbilityCasted();
            }
        }
    }

    protected virtual void OnAbilityCasted() { }
    private IEnumerator Cooldown()
    {
        float speed = 1 / _coolDown;
        IsOnCooldown = true;

        while (_elapsedTime > 0)
        {
            _elapsedTime = Mathf.MoveTowards(_elapsedTime, 0, speed * Time.deltaTime);

            yield return null;
        }

        _elapsedTime = 1;
        IsOnCooldown = false;
    }
}
