using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    Shoot,
}

public abstract class AbilityBase : MonoBehaviour
{
    public event Action<float> OnUseAbility;

    [SerializeField] private AbilityDataBase _data;

    private bool _canUse = true;

    public void UseAbility()
    {
        if (_canUse)
        {
            OnUseAbility?.Invoke(_data.CooldownTime);
            Ability();
            StartCooldown();
        }
    }

    protected abstract void Ability();

    private void StartCooldown()
    {
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        _canUse = false;
        yield return new WaitForSeconds(_data.CooldownTime);
        _canUse = true;
    }

}
