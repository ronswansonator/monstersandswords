using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public int HealAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if( other.tag == "Player")
        {
            other.gameObject.GetComponent<Player>()?.Heal(HealAmount);
            Destroy(gameObject);
        }
    }
}
