using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    NavMeshAgent _agent;
    GameObject _playerRef;
    public int MaxHealth = 3;
    private int _currHealth;
    public int AttackDamage = 1;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _currHealth = MaxHealth;
    }

    private void Start()
    {
        _playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        // Feel like setting the destination every frame is bad, need to look more into if this is ok
        _agent.SetDestination(_playerRef.transform.position);
    }

    public void TakeDamage(int damageAmount)
    {
        _currHealth -= damageAmount;
        if( _currHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject); // just destroy self for now
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject == _playerRef)
        {
            _playerRef.GetComponent<Player>()?.TakeDamage(AttackDamage);
        }
    }
}
