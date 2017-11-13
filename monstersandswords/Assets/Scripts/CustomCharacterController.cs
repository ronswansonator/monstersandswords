using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class CustomCharacterController : MonoBehaviour
{
    private CharacterMotor _motor;

    public float AttackRange = .75f;
    public float AttackRadius = .5f;

    void Awake()
    {
        _motor = GetComponent<CharacterMotor>();
    }

    void Update()
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
        if (Input.GetMouseButtonDown(0))
        {
            Collider[] hits = Physics.OverlapSphere(transform.position + transform.forward * AttackRange, AttackRadius);
            foreach (Collider hit in hits)
            {
                if( hit.gameObject.tag == "Enemy")
                {
                    Destroy(hit.gameObject);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Color temp = Gizmos.color;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * AttackRange, AttackRadius);
        Gizmos.color = temp;
    }
}
