using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class CustomCharacterController : MonoBehaviour
{
    private CharacterMotor _motor;
    public Weapon CurrWeapon;

    private void Awake()
    {
        _motor = GetComponent<CharacterMotor>();
    }

    private void Start()
    {
        UpdateCursorState(true); 
    }

    private void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    void HandleMovement()
    {
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

        _motor.Move(dirToMove * Time.deltaTime);
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
