using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    [SerializeField] private float _cooldownTime;

    private bool _canUse = true;

    public void UseAbility()
    {
        if (_canUse)
        {
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
        yield return new WaitForSeconds(_cooldownTime);
        _canUse = true;
    }

}
