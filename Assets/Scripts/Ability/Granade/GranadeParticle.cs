using UnityEngine;

public class GranadeParticle : MonoBehaviour
{
    [SerializeField] private int _maxImpactCount = 3;
    [SerializeField] private ParticleSystem _vfxGranadeImpact;
    [SerializeField] private ParticleSystem _vfxGranadeExplosion;

    private ParticleSystem _vfxGranade;
    private ParticleSystem.Particle[] _granades;
    private int _impactCount = 0;


    private void Start()
    {
        _vfxGranade = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (UpdateParticlesData() <= 0) return;

        if (other.layer == LayerMask.NameToLayer("Ground"))
        {
            Vector3 impactPosition = new Vector3(_granades[0].position.x, _granades[0].position.y * .1f, _granades[0].position.z);
            Instantiate(_vfxGranadeImpact, impactPosition, Quaternion.identity);
        }

        _impactCount++;
        if (_impactCount >= _maxImpactCount)
        {
            Instantiate(_vfxGranadeExplosion, _granades[0].position, Quaternion.identity);
            Destroy(_vfxGranade.gameObject);
        }
    }

    private int UpdateParticlesData()
    {
        _granades = new ParticleSystem.Particle[_vfxGranade.main.maxParticles];
        return _vfxGranade.GetParticles(_granades);
    }
}