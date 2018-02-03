using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance = null;
    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    public void TakeDamage()
    {
        DamageTaken.SetActive(true);
        Invoke("TurnOffDamage", .25f);
    }
    public void TurnOffDamage()
    {
        DamageTaken.SetActive(false);
    }

    public GameObject GameOver;
    public GameObject DamageTaken;
}
