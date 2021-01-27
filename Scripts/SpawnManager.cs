using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject points;
    [SerializeField]
    private Transform transformPlayer;
    [SerializeField]
    private List<GameObject> bodyPartsObject = new List<GameObject>();
    [SerializeField]
    private int partsBodySnake = 0;
    [SerializeField]
    private Transform playerHead;
    [SerializeField]
    public bool gameOver=true;
    private UIManager _UImanager;
    [SerializeField]
    private GameObject player_Head;
    [SerializeField]
    private AudioSource _SoundGameBgc;
    // Start is called before the first frame update
    void Start()
    {
        SoundBGC();
        _UImanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        gameOver = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartAll()
    {
        if (player_Head.active == false)
        {
            gameOver = false;
            StartCoroutine(PointsSpawnRoutine());
            _UImanager.checkScoreBody = 4;
            _UImanager.score = 0;
            partsBodySnake = 0;
            _UImanager.scoreText.text = "Score: " + _UImanager.score;
            player_Head.transform.position = new Vector3(0f, 158f, -0.546875f);
            player_Head.SetActive(true);
        }
    }
    public void ReloadAll()
    {
        SceneManager.LoadScene("Menu");
        /*if (player_Head.active == false)
        {
            gameOver = false;
            StartCoroutine(PointsSpawnRoutine());
            _UImanager.checkScoreBody = 4;
            _UImanager.score = 0;
            partsBodySnake = 0;
            _UImanager.scoreText.text = "Score : " + _UImanager.score;
            player_Head.transform.position = new Vector3(5.45f, 17.2f, -0.546875f);
            player_Head.SetActive(true); 
        }
        */
    }
    public void ExitApplication()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
		Application.Quit();
    #endif
    }
    IEnumerator PointsSpawnRoutine()
    {
        while(gameOver==false)
        {
            Instantiate(points, new Vector3(Random.RandomRange(-448f, 483f), Random.RandomRange(412f, -80f), 0), Quaternion.identity);
            yield return new WaitForSeconds(3.0f);
        }
        
    }
    public void AddSnakeBody()
    {
        if (partsBodySnake >= 25)
            return;
        bodyPartsObject[partsBodySnake].transform.position = new Vector3(playerHead.transform.position.x, playerHead.transform.position.y, playerHead.transform.position.z);
        bodyPartsObject[partsBodySnake].SetActive(true);
        partsBodySnake++;
    }
    public void DelSnakeBody()
    {
        while(partsBodySnake>=0)
        {
            bodyPartsObject[partsBodySnake].SetActive(false);
            partsBodySnake--;
            gameOver = true;
        }
        
    }
    private void SoundBGC()
    {
        _SoundGameBgc.volume=PlayerPrefs.GetFloat("SoundGame");
        //Debug.Log(PlayerPrefs.GetFloat("SoundGame"));
    }

}
