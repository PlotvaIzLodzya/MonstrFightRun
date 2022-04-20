using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    [SerializeField] private LayerMask _ground;
    [SerializeField] private LayerMask _enemy;
    [SerializeField] private float _radius;
    [SerializeField] private float _time;

    private bool _collided;
    private float _damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground ground))
        {
            Exploode();
            _collided = true;
        }
    }

    public void Init(float damage)
    {
        Vector3 destination;

        _damage = damage;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit raycastHit, _ground))
        {
            destination = raycastHit.point;

            StartCoroutine(FlyAnimation(destination));
        }
    }

    private void Exploode()
    {
        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, _radius, _enemy);

        foreach (var collider in enemyColliders)
        {
            if(collider.TryGetComponent(out Monster monster))
            {
                Debug.Log(monster.name);
                monster.ApplyDamage(_damage);
            }
        }
    }

    private IEnumerator FlyAnimation(Vector3 destination)
    {
        float speed = Vector3.Distance(transform.position, destination) / _time;
        Vector3 directaion = destination - transform.position;


        while(_collided == false)
        {
            transform.position += directaion.normalized * speed * Time.deltaTime;

            yield return null;
        }
    }
}
