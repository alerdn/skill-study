using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlller : MonoBehaviour
{
    public CharacterController CharacterController => _characterController;

    private const float GRAVITY = 9.87f;

    [SerializeField] private float _mass = 2f;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private AbilityMap[] _abilitiesMap;

    private CharacterController _characterController;
    private Rigidbody _rigidbody;
    private Camera _cam;
    private float _turnSmoothVelocity;
    private float _verticalSpeed;

    private void Start()
    {
        _cam = Camera.main;
        _characterController = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Ability();
        Movement();
        Aim();
    }

    #region Abilities

    private void Ability()
    {
        foreach (AbilityMap abilityMap in _abilitiesMap)
        {
            if (Input.GetKey(abilityMap.KeyCode))
            {
                abilityMap.Ability.UseAbility();
            }
        }
    }

    public void Dash(float multiplier, float duration)
    {
        Vector3 speedVector = CalculateSpeedVector();

        Vector3 dashVector = new Vector3(speedVector.x * multiplier, speedVector.y, speedVector.z * multiplier);
        StartCoroutine(DashRoutine(dashVector, duration));
    }

    private IEnumerator DashRoutine(Vector3 direction, float duration)
    {
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            _characterController.Move(direction * Time.deltaTime);
            yield return null;
        }
    }

    #endregion

    private void Movement()
    {
        Vector3 speedVector = CalculateSpeedVector();

        _characterController.Move(speedVector * Time.deltaTime);
    }

    private Vector3 CalculateSpeedVector()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cam.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, .1f);
        Vector3 speedVector = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized * _moveSpeed;

        bool isWalking = direction.magnitude >= .1f;
        if (isWalking)
        {
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        else
        {
            speedVector = Vector3.zero;
        }

        _verticalSpeed -= GRAVITY * _mass * Time.deltaTime;
        speedVector.y = _verticalSpeed;

        return speedVector;
    }

    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            var direction = position - transform.position;
            direction.y = 0;

            transform.forward = direction;
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = _cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, _groundMask))
        {
            return (success: true, position: hitInfo.point);
        }
        else
        {
            return (success: false, position: Vector3.zero);
        }
    }
}

[System.Serializable]
public class AbilityMap
{
    public KeyCode KeyCode;
    public AbilityBase Ability;
}
