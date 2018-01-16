using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomCharacterController : MonoBehaviour
{
    public Weapon CurrWeapon;

    public float MoveSpeed = 10.0f;
    Rigidbody _rigidbody;

    public float DashSpeed = 20.0f;
    public float DashDuration = .75f;
    public float DashCoolDown = .75f;

    private float _dashTimer = 0;
    private bool _isDashing = false;
    private Vector2 _dashDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _dashTimer = DashCoolDown;
    }

    private void Start()
    {
        UpdateCursorState(true); 
    }

    private void Update()
    {
        HandleAttack();
    }

    private void FixedUpdate()
    {
        HandleDash();
        HandleMovement();
    }

    void HandleMovement()
    {
        if (_isDashing)
            return;

        Vector2 dirToMove = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            dirToMove += transform.forward.ToVec2XZ();
        }
        if (Input.GetKey(KeyCode.A))
        {
            dirToMove -= transform.right.ToVec2XZ();
        }
        if (Input.GetKey(KeyCode.S))
        {
            dirToMove -= transform.forward.ToVec2XZ();
        }
        if (Input.GetKey(KeyCode.D))
        {
            dirToMove += transform.right.ToVec2XZ();
        }

        if(dirToMove.IsFuzzyZero())
        {
            _dashDirection = transform.forward.ToVec2XZ();
        }
        else
        {
            _dashDirection = dirToMove;
        }

        Move(dirToMove * Time.fixedDeltaTime, MoveSpeed);
    }

    void HandleDash()
    {
        _dashTimer += Time.fixedDeltaTime;
        if (_isDashing)
        {
            if (_dashTimer < DashDuration)
            {
                Move(Time.fixedDeltaTime * _dashDirection, DashSpeed);
            }
            else
            {
                _dashTimer = 0;
                _isDashing = false;
            }
        }
        else
        {
            if (_dashTimer >= DashCoolDown)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    _isDashing = true;
                    _dashTimer = 0;
                }
            }
        }
    }

    public void Move(Vector2 dir, float speed)
    {
        _rigidbody.MovePosition(_rigidbody.position + dir.ToVec3XZ() * speed);
    }

    void HandleAttack()
    {
        if (CurrWeapon)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CurrWeapon.Attack();
            }
        }
    }

    void UpdateCursorState(bool playing)
    {
        Cursor.lockState = playing ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !playing;
    }
}
