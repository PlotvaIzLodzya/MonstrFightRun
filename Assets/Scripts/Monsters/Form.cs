using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Form : MonoBehaviour
{
    private Animator _animator;
    private Monster _monster;

    public Animator FormAnimator => _animator;

    private const float _layerMaxWeight = 1f;

    private void Awake()
    {
        _monster = transform.root.GetComponent<Monster>();
        _animator = GetComponent<Animator>();
    }

    public void ChangeAnimatorLayer(int index)
    {
        if (index > 0)
            StartCoroutine(LayerWeightChange(index));
    }

    private IEnumerator LayerWeightChange(int index)
    {
        float weight = 0.5f;
        float changeSpeed = _layerMaxWeight / 0.2f;

        while (weight < _layerMaxWeight)
        {
            weight = Mathf.MoveTowards(weight, _layerMaxWeight, changeSpeed * Time.deltaTime);

            _animator.SetLayerWeight(index, weight);

            yield return null;
        } 
    }

    private void OnAttack()
    {
        if(_monster.IsAllive)
            _monster.GiveDamage();
    }
}
