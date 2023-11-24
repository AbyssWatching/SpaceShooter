using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public Player _player;
    [SerializeField] private TMP_Text _scoreText;
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
}
