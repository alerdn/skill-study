using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlller : MonoBehaviour
{
    private const float GRAVITY = 9.87f;

    [SerializeField] private float _mass = 2f;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;

    private CharacterController _characterController;
    private Camera _cam;
    private float _turnSmoothVelocity;
    private float _verticalSpeed;

    private void Start()
    {
        _cam = Camera.main;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
        HandleAim();
    }

    private void HandleMovement()
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

        if (_characterController.isGrounded)
        {
            _verticalSpeed = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _verticalSpeed = _jumpForce;
            }
        }

        _verticalSpeed -= GRAVITY * _mass * Time.deltaTime;
        speedVector.y = _verticalSpeed;

        _characterController.Move(speedVector * Time.deltaTime);
    }

    private void HandleAim()
    {
        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1)) return;

        Vector3 inputPosition = Input.mousePosition;
        float distanceFromCamera = Vector3.Distance(_cam.transform.position, transform.position);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, distanceFromCamera));

        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
}
