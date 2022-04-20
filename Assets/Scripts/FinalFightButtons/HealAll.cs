using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAll : AbilitiyButtonView
{
    [SerializeField] private ParticleSystem _particleSystemPrefab;

    private IEnumerable<Monster> _monsters;

    public override void Cast()
    {
        if(_monsters == null)
            _monsters = FindObjectOfType<MonstersHandler>().GetAllMonsters();

        foreach (var monster in _monsters)
        {
            monster.Health.Increase(ValueHandler.Amount);
            PlayParticle(monster.transform.position);
        }
    }

    private void PlayParticle(Vector3 position)
    {
        Instantiate(_particleSystemPrefab, position, _particleSystemPrefab.transform.rotation);
    }
}
