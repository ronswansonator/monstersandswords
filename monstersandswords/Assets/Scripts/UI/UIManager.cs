using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject GameOver;
    public GameObject DamageTaken;
    private Player _player = null;
    public Player Player { get { return _player; } }
    public UIHealth Health;
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
        UpdateHealth();
        DamageTaken.SetActive(true);
        Invoke("TurnOffDamage", .25f);
    }
    public void TurnOffDamage()
    {
        DamageTaken.SetActive(false);
    }
    public void SetPlayer(Player p)
    {
        _player = p;
        Health.UpdatePlayerHealth();
    }
    public void UpdateHealth()
    {
        Health.UpdatePlayerHealth();
    }


}
