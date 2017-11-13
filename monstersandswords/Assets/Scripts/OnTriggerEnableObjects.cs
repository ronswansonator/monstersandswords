using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnableObjects : MonoBehaviour
{
    public string TagToTriggerOffOf = "Player";
    public GameObject[] ToSet;
    public bool ToSetTo;

    void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.tag == TagToTriggerOffOf )
        {
            foreach( GameObject gO in ToSet )
            {
                gO.SetActive(ToSetTo);
            }
        }
    }
}
