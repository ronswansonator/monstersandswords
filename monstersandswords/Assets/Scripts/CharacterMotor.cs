using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMotor : MonoBehaviour
{
    public float MoveSpeed = 10.0f;
    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move( Vector2 dir )
    {
        _rigidbody.MovePosition(_rigidbody.position + dir.ToVec3XZ() * MoveSpeed);
    }
}
