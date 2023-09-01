using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledParticleSystemCallback : MonoBehaviour
{
    public IObjectPool<ParticleSystem> Pool;

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
}
