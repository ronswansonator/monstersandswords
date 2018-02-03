using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int MaxHealth = 3;
    private int _currentHealth = 3;

    private void Awake()
    {
        _currentHealth = MaxHealth;
        GroupBrain.Instance.SetPlayer(this);
    }

    public void Heal(int healAmount)
    {
        _currentHealth += healAmount;
        _currentHealth = _currentHealth > MaxHealth ? MaxHealth : _currentHealth; // Clamps to max health to prevent over-healing
    }

    public void TakeDamage(int damageAmount)
    {
        _currentHealth -= damageAmount;
        if( _currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject); // TODO: Go to death screen
    }
}
