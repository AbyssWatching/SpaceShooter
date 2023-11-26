using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Player _player;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Sprite[] _livesDisplay;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _gameOver;
    [SerializeField] private float _flickerTiming = .5f;
    private bool _restartGame = false;
    // Start is called before the first frame update
    void Start()
    {
        // _player = GameObject.Find("Player").GetComponent<Player>();

        _scoreText.text = "Score: " + 0;


    }
    //updates score
    public void updateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    //updates visual of lives
    public void updateLives(int currentLives) 
    {
        _image.sprite = _livesDisplay[currentLives];

        if (currentLives == 0)
        {
            _gameOver.gameObject.SetActive(true);

            StartCoroutine(FlickerSequence());
        }
    }
    
    // makes the gameover flicker
    private IEnumerator FlickerSequence() 
    {
        while (_restartGame == false)
        {
            yield return new WaitForSeconds(_flickerTiming);
            _gameOver.text = "Game Over";

            yield return new WaitForSeconds(_flickerTiming);
            _gameOver.text = "";

            // if (Input.GetKeyDown(KeyCode.R))
            // {
            //     _restartGame = true;
            //     Debug.Log("someone's pressing r punk");
            // }
        }
    }
}
