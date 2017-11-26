using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health = 3;
    private int _currentHealth = 3;

    private void Awake()
    {
        _currentHealth = Health;
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
