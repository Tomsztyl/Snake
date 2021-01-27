using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public int score;
    [SerializeField]
    public int bestScoreUI;
    [SerializeField]
    private Text bestScore;
    [SerializeField]
    public Text scoreText;
    [SerializeField]
    public int checkScoreBody=4;
    [SerializeField]
    private SpawnManager _spawnManager;
    [SerializeField]
    private ControllsLogin _controllsLogin;
    // Start is called before the first frame update
    void Start()
    {
        //_spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _controllsLogin = GameObject.Find("ControllsLogin").GetComponent<ControllsLogin>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score;
        if (score>bestScoreUI)
        {
            UpdateBestScore();
        }
        if (score==checkScoreBody)
        {
            checkScoreBody +=4;
            _spawnManager.AddSnakeBody();
        }
        
    }
    private void UpdateBestScore()
    {
            bestScoreUI = score;
            _controllsLogin.bestScore = bestScoreUI;
            _controllsLogin.SetUpdateScore();
            _controllsLogin.UpdateBestScore();
    }
    
}
