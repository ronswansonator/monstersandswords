using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GroupBrain
{
    private static GroupBrain _instance = null;
    public static GroupBrain Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GroupBrain();
            }
            return _instance;
        }
    }
    private Player _player = null;
    public Player Player { get { return _player; }}
    private List<Enemy> _enemies = new List<Enemy>();
    private List<Enemy> _enemiesInRadius = new List<Enemy>();
    private float _attackTime = 1f;
    private float _attackTimer = 0;

    private float AttackRange = 2.0f;
    public void Update()
    {
        if (_player != null)
        {
            foreach (var enemy in _enemies)
            {
                if (IsEnemyInRadius(enemy))
                {
                    if (!_enemiesInRadius.Any(x => x == enemy))
                    {
                        _enemiesInRadius.Add(enemy);
                    }
                    if (enemy.IsAttacking && DistanceBetweenEnemeyAndDestination(enemy) <= AttackRange)
                    {
                        enemy.Attack();
                        enemy.SetAttack(false);
                    }
                    else if(enemy.IsAttacking == false && !enemy.CanAttack())
                    {
                        enemy.destination = GetClosestCirclePoint(_player.transform.position.ToVec2XZ(), enemy.transform.position.ToVec2XZ()).ToVec3XZ();
                    }

                }
                else
                {
                    _enemiesInRadius.Remove(enemy);
                    enemy.SetAttack(false);
                    enemy.destination = GetClosestCirclePoint(_player.transform.position.ToVec2XZ(), enemy.transform.position.ToVec2XZ()).ToVec3XZ();
                }
            }
            if (_attackTimer <= 0)
            {
                foreach (var e in _enemiesInRadius)
                {
                    if (e.CanAttack() && !e.IsAttacking)
                    {
                        e.SetAttack(true);
                        e.destination = _player.transform.position;
                        _attackTimer = _attackTime;
                        break;
                    }
                }
            }
            else
            {
                _attackTimer -= Time.deltaTime;
            }

        }
    }
    public void SceneInit()
    {
        _enemies.Clear();
        _enemiesInRadius.Clear();
    }
    public float TacticalRingRadius = 5.0f;
    public bool IsEnemyInRadius(Enemy e)
    {
        return DistanceBetweenEnemeyAndDestination(e) < TacticalRingRadius * TacticalRingRadius + 1;
    }
    public float DistanceBetweenEnemeyAndDestination(Enemy e)
    {
        if (e.destination.HasValue && e != null)
        {
            return Vector3.SqrMagnitude(e.transform.position - e.destination.Value);
        }
        return -1;
    }

    public void OnEnemyActive(Enemy target)
    {
        _enemies.Add(target);
        //if (_enemies.Count > 1)
        //{
            target.MoveTowardsPlayer = true;
           
        //}
    }
    public void OnEnemeyInactive(Enemy target)
    {
        _enemies.Remove(target);
        _enemiesInRadius.Remove(target);
    }
    public void SetPlayer(Player p)
    {
        _player = p;
    }
    public Vector2 GetClosestCirclePoint(Vector2 origin, Vector2 testPoint)
    {
        return (testPoint - origin).normalized * TacticalRingRadius + origin;
    }
}
