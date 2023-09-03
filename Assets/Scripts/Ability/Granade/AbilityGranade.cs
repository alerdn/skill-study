using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityGranade : AbilityBase
{
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private ParticleSystem _vfxGranade;

    protected override void Ability()
    {
        Instantiate(_vfxGranade, _shootPosition);
    }
}
