using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    NavMeshAgent _agent;
    public int MaxHealth = 3;
    private int _currHealth;
    public int AttackDamage = 1;
    public bool MoveTowardsPlayer = false;
    public float AttackRange = 2.0f;
    public Vector3? destination = null;
    private float _lastAttackTime = 0;
    public float AttackCooldownTime = 2.0f;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _currHealth = MaxHealth;
        GroupBrain.Instance.OnEnemyActive(this);
    }

    private void Start()
    {
    }

    private void Update()
    {
        // Feel like setting the destination every frame is bad, need to look more into if this is ok
        if (MoveTowardsPlayer && GroupBrain.Instance.Player != null && destination.HasValue)
        {
            _agent.SetDestination(destination.Value);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        _currHealth -= damageAmount;
        if (_currHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GroupBrain.Instance.OnEnemeyInactive(this);
        Destroy(gameObject); // just destroy self for now
    }
    public void OnDestroy()
    {
        GroupBrain.Instance.OnEnemeyInactive(this);
    }
    public void Attack()
    {
        if (Time.time - _lastAttackTime > AttackCooldownTime)
        {
            var colliders = Physics.OverlapSphere(transform.position + transform.forward * AttackRange, 1.0f);
            if (colliders.Any(x => x.gameObject.tag == "Player"))
            {
                GroupBrain.Instance.Player.TakeDamage(AttackDamage);
            }
            _lastAttackTime = Time.time;
        }
    }
}
