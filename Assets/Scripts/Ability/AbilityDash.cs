using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDash : AbilityBase
{
    [SerializeField] private PlayerControlller _player;
    [SerializeField] private float _dashMultiplier = 10f;
    [SerializeField] private float _dashDuration = 1f;

    protected override void Ability()
    {
        _player.Dash(_dashMultiplier, _dashDuration);
    }
}
