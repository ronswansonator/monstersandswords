using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int DamageAmount = 1;
    public string TriggerName = "HitRequest";
    public Animator _anim;

    public void Attack()
    {
        _anim.SetTrigger(TriggerName);
    }

    void OnTriggerEnter(Collider other)
    {
        if( other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>()?.TakeDamage(DamageAmount);
        }
    }
}
