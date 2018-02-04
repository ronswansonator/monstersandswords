using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int DamageAmount = 1;
    public string ClipName = "";
    Animation _anim; // TODO: Update to mecanim

    private void Start()
    {
        _anim = GetComponent<Animation>();
    }

    public void Attack()
    {
        if (!_anim.isPlaying)
        {
            _anim.Play(ClipName);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if( other.tag == "Enemy" && _anim.isPlaying)
        {
            other.gameObject.GetComponent<Enemy>()?.TakeDamage(DamageAmount);
        }
    }
}
