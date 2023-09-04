using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeAreaOfDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        DamageComponent damageable = other.transform.GetComponentInChildren<DamageComponent>();
        if (damageable)
        {
            damageable.TakeDamage(5);
        }
    }
}
