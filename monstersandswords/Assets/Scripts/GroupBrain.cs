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
    private float AttackRange = 2.0f;
    public void Update()
    {
        if (_player != null)
        {
            foreach (var enemy in _enemies)
            {
                if (IsEnemyInRadius(enemy))
                {
                    enemy.destination = _player.transform.position;
                    if (DistanceBetweenEnemeyAndPlayer(enemy) <= AttackRange)
                    {
                        enemy.Attack();
                    }
                }
                else
                {
                    enemy.destination = GetClosestCirclePoint(_player.transform.position.ToVec2XZ(), enemy.transform.position.ToVec2XZ()).ToVec3XZ();
                }
            }
        }
    }

    public float TacticalRingRadius = 5.0f;
    public bool IsEnemyInRadius(Enemy e)
    {
        return DistanceBetweenEnemeyAndPlayer(e) < TacticalRingRadius * TacticalRingRadius;
    }
    public float DistanceBetweenEnemeyAndPlayer(Enemy e)
    {
        if (_player != null && e != null)
        {
            return Vector3.SqrMagnitude(e.transform.position - _player.transform.position);
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
