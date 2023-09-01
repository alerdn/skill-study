using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AbilityShoot : AbilityBase
{
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private ParticleSystem _bulletPrefab;

    private IObjectPool<ParticleSystem> _bulletPool;
    private int _maxPoolSize = 20;

    private void Start()
    {
        _bulletPool = new LinkedPool<ParticleSystem>(OnCreateBullet, OnTakeFromPool, OnReturnToPool, OnDestroyBullet, true, _maxPoolSize);
    }

    #region Bullet Pool

    private ParticleSystem OnCreateBullet()
    {
        var ps = Instantiate(_bulletPrefab, _shootPosition);
        ps.gameObject.SetActive(false);

        ps.GetComponent<PooledParticleListener>().Pool = _bulletPool;

        return ps;
    }

    private void OnTakeFromPool(ParticleSystem ps)
    {
        ps.gameObject.SetActive(true);
    }

    private void OnReturnToPool(ParticleSystem ps)
    {
        ps.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(ParticleSystem ps)
    {
        Destroy(ps.gameObject);
    }

    #endregion

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            UseAbility();
        }
    }

    protected override void Ability()
    {
        var bullet = _bulletPool.Get();
        bullet.Play();
    }
}
