using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    protected int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        int newHealth = _currentHealth - damage;
        _currentHealth = Mathf.Clamp(newHealth, 0, _maxHealth);

        if (_currentHealth == 0) Kill();
    }

    protected virtual void Kill()
    {
        Destroy(gameObject);
    }
}
