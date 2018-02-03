using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GroupBrain : MonoBehaviour
{
    private static GroupBrain _instance = null;
    public static GroupBrain Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject g = new GameObject("GroupBrain");
                _instance = g.AddComponent<GroupBrain>();
                DontDestroyOnLoad(g);
            }
            return _instance;
        }
    }
    private Player _player = null;
    public Player Player { get { return _player; }}
    private List<Enemy> _enemies = new List<Enemy>();

    private void Update()
    {
    }

    public float TacticalRingRadius = 5.0f;
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
