using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    Text _textComp;
    public void Awake()
    {
        _textComp = GetComponent<Text>();
        _textComp.text = $"GAME OVER";
    }
    public void Replay()
    {
        //GameObject.FindGameObjectWithTag("GameOver").transform.GetChild(0).gameObject.SetActive(false);
        GameManager.Instance.Replay();
    }
}
