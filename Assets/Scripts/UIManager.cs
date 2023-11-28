using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _livesDisplay;
    [SerializeField] private TMP_Text _restartText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private float _flickerTiming = .5f;
    private bool _gameOverBool = false;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        //setting up Score TMP
        _scoreText.text = "Score: " + 0;
    }

       void Update()
    {
        //restarts game if it is over
        if (_gameOverBool == true && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SpaceShooterGame");
        }
    }

    //updates score
    public void updateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    //updates visual of lives and what to do when player dies
    public void updateLives(int currentLives) 
    {
        _image.sprite = _livesDisplay[currentLives];

        if (currentLives == 0)
        {
            _gameOverBool = true;
            _gameOverText.gameObject.SetActive(true);
            _restartText.gameObject.SetActive(true);

            StartCoroutine(FlickerSequence());
        }
    }
    
    // makes the gameover flicker
    private IEnumerator FlickerSequence() 
    {
        while (_gameOverBool == true)
        {
            yield return new WaitForSeconds(_flickerTiming);
            _gameOverText.text = "Game Over";

            yield return new WaitForSeconds(_flickerTiming);
            _gameOverText.text = "";

        }
    }
}
