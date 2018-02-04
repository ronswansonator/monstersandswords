using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour {

    Text _textComp;
    public void Awake()
    {
        _textComp = GetComponent<Text>();
    }
    public void UpdatePlayerHealth()
    {
        _textComp.text = $"Health: {UIManager.Instance.Player.GetHealth()}";
    }
}
