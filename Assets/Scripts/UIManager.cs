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

    public void updateLives(int currentLives) 
    {
        _image.sprite = _livesDisplay[currentLives];
    }
}
