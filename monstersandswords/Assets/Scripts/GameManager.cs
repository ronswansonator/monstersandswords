using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                //GameObject g = new GameObject("GameManager");
                //_instance = g.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    // Use this for initialization
    void Start()
    {
        UpdateCursorState(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GameOver()
    {
        UIManager.Instance.GameOver.SetActive(true);
        Time.timeScale = 0;
        UpdateCursorState(false);
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        UpdateCursorState(true);
    }
    void UpdateCursorState(bool playing)
    {
        Cursor.lockState = playing ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !playing;
    }
}
