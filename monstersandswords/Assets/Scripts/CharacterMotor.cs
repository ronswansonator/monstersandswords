using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMotor : MonoBehaviour
{
    public float MoveSpeed = 10.0f;

    public void Move( Vector2 dir )
    {
        transform.position += dir.ToVec3XZ() * MoveSpeed;
    }
}
