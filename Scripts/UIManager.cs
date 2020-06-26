using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public int score;
    [SerializeField]
    public Text scoreText;
    [SerializeField]
    public int checkScoreBody=4;
    [SerializeField]
    private SpawnManager _spawnManager;
    public int scoreTest=5;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score;
        if (score==checkScoreBody)
        {
            checkScoreBody +=4;
            _spawnManager.AddSnakeBody();
        }
        
    }
    
}
