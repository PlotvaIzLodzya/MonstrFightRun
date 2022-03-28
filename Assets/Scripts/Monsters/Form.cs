using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator),typeof(RagdollHandler))]
public class Form : MonoBehaviour
{
    [SerializeField] private Monster _monster;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Transform _particlePoint;

    private Animator _animator;
    private RagdollHandler _ragdollHandler;

    public SkinnedMeshRenderer _skinnedMeshRenderer { get; private set; }
    public Animator FormAnimator => _animator;

    private Coroutine _coroutine;

    private void Awake()
    {
        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _animator = GetComponent<Animator>();
        _ragdollHandler = GetComponent<RagdollHandler>();

        _skinnedMeshRenderer.material.SetFloat("_SelfShadingSizeExtra", 1f);
    }

    public void EnableRagdoll()
    {
        _ragdollHandler.EnableRagdoll();
    }

    private void RangeAttack()
    {
        if (_monster.IsAllive)
        {
            _monster.DealDamage();

            if(_particleSystem != null)
                Instantiate(_particleSystem, _particlePoint.position, _particlePoint.rotation);
        }
    }

    public void OnDamaged()
    {
        _skinnedMeshRenderer.material.SetFloat("_SelfShadingSizeExtra", 1f);

        if (_coroutine != null)
            _coroutine = null;

        _coroutine =StartCoroutine(MagicalMaterial());
    }

    private IEnumerator MagicalMaterial()
    {
        yield return new WaitForSeconds(0.01f);

        _skinnedMeshRenderer.material.SetFloat("_SelfShadingSizeExtra", 0f);
    }

    private void Run() { }

}
