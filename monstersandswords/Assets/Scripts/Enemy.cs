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
    public float AttackCooldownTime = 1.0f;
    bool _isAttacking = false;
    public bool IsAttacking => _isAttacking;
    public float AttackBoostSpeed = 2.0f;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _currHealth = MaxHealth;
        GroupBrain.Instance.OnEnemyActive(this);
    }
    private void Update()
    {
        if (GetComponent<MeshRenderer>().material.color != Color.green)
        {
            if (IsAttacking)
            {
                GetComponent<MeshRenderer>().material.color = Color.black;
            }
            else
            {
                GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
        // Feel like setting the destination every frame is bad, need to look more into if this is ok
        if (MoveTowardsPlayer && GroupBrain.Instance.Player != null && destination.HasValue)
        {
            _agent.SetDestination(destination.Value);
        }
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

    public bool CanAttack()
    {
        return (Time.time - _lastAttackTime) > AttackCooldownTime;
    }
    public void SetAttack(bool isAttack)
    {
        if (_isAttacking != isAttack)
        {
            if (isAttack)
            {
                _agent.speed += AttackBoostSpeed;
            }
            else
            {
                _agent.speed -= AttackBoostSpeed;
            }
            _isAttacking = isAttack;
        }
    }
    public void ChangeColor()//Just for testing
    {
        GetComponent<MeshRenderer>().material.color = Color.green;
    }
    public void ChangeColorBack()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }
    public void TakeDamage(int damageAmount)
    {
        ChangeColor();
        Invoke("ChangeColorBack", .25f);
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
}
