using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class SeekObjectWithTag : MonoBehaviour
{
    public string TagToSeek = "Player";

    private Transform _target;
    private CharacterMotor _motor;

    void Awake()
    {
        _motor = GetComponent<CharacterMotor>();
    }

	void Start ()
    {
        _target = GameObject.FindWithTag(TagToSeek)?.transform;
        if( !_target)
        {
            Debug.LogError("Can't find seek target! No object in scene with tag: " + TagToSeek);
        }
	}
	
	void Update ()
    {
        _motor.Move((_target.position - transform.position).normalized.ToVec2XZ() * Time.deltaTime);
	}
}
