using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private AbilityBase _ability;

    private Transform _target;

    private void Start()
    {
        _target = GameObject.FindObjectOfType<PlayerControlller>()?.transform;
    }

    private void Update()
    {
        transform.LookAt(_target);
        _ability.UseAbility();
    }
}
