using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _gameOverBool = false;
    
    void Start()
    {
        Player.onPlayerDeath += GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        WaitForRestart();
        EscapeGame();
    }
    //just getting info could have used an event
    public void GameOver()
    {
        _gameOverBool = true;
    }
    //method for restarting
    private void WaitForRestart()
    {
           if (_gameOverBool == true && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
    private void EscapeGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }
    }
}
