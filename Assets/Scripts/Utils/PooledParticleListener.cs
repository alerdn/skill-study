using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledParticleListener : MonoBehaviour
{
    public IObjectPool<ParticleSystem> Pool;

    [SerializeField] private ParticleSystem _explosionPrefab;

    private ParticleSystem _ps;

    private void Start()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleSystemStopped()
    {
        if (Pool != null)
            Pool.Release(_ps);
    }

    private void OnParticleCollision(GameObject other)
    {
        Instantiate(_explosionPrefab, other.transform.position, Quaternion.identity);

        DamageComponent damageable = other.transform.GetComponentInChildren<DamageComponent>();
        if (damageable)
        {
            damageable.TakeDamage(1);
        }
    }
}
