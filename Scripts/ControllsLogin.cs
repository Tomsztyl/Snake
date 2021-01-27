using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllsLogin : MonoBehaviour
{
    [SerializeField] private Text TextUserButton;
    [SerializeField] private Text TextBestScore;

    [SerializeField] public int bestScore=0;
    [SerializeField] private string bestScoreUIString;

    [SerializeField] private Text TextButtonPostLog;
    [SerializeField] private GameObject ButtonPostLog;

    [SerializeField] private float CheckDelayLoginTimeout=10f;
    [SerializeField] private bool CheckEndDelayLogin =false;


    private void Start()
    {
        TextUserButton.text = PlayerPrefs.GetString("UsernameLogin");
        
        Scene _actualScene = SceneManager.GetActiveScene();
        if (_actualScene.name == "GameScene")
        {
            UpdateBestScore();
        }
        //SetUpdateScore();
    }
    private void Update()
    {
        if (CheckEndDelayLogin==false)
        StartCoroutine(DelayLogin());
    }

    public void LogoutMenu()
    {
        Scene _actualScene = SceneManager.GetActiveScene();
        if (_actualScene.name!="Lobby")
        {
            PlayerPrefs.SetString("UsernameLogin", "");
            PlayerPrefs.SetString("UsernamePassword", "");
            SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
        }
        
    }
    private void CheckLogin()
    {
        StartCoroutine(CheckDelayLogin(PlayerPrefs.GetString("UsernameLogin"), PlayerPrefs.GetString("UsernamePassword")));
    }
    IEnumerator DelayLogin()
    {
        Debug.Log("Started Delay Check Login");
        CheckEndDelayLogin = true;
        yield return new WaitForSeconds(CheckDelayLoginTimeout);
        CheckLogin();
        CheckEndDelayLogin = false;
        Debug.Log("End Delay Login");
    }
    IEnumerator CheckDelayLogin(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("https://unityfsadsa.000webhostapp.com/gameUnity/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
                ButtonPostLog.active = true;
                TextButtonPostLog.text = www.error;
            }
            else
            {
                if (www.downloadHandler.text!= "Login Success")
                {
                    ButtonPostLog.active = true;
                    TextButtonPostLog.text = www.downloadHandler.text;
                }
                else
                {
                    if (ButtonPostLog.activeSelf == true)
                        ButtonPostLog.active = false;
                }
            }
        }
    }
    public void UpdateBestScore()
    {
        Scene _actualScene = SceneManager.GetActiveScene();
        if (_actualScene.name== "GameScene")
        {
            TextBestScore = GameObject.FindGameObjectWithTag("BestScore").GetComponent<Text>();
            StartCoroutine(GetScore(PlayerPrefs.GetString("UsernameLogin"), PlayerPrefs.GetString("UsernamePassword")));
        }
    }
    IEnumerator GetScore(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("https://unityfsadsa.000webhostapp.com/gameUnity/GetScore.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                ButtonPostLog.active = true;
                TextButtonPostLog.text = www.error;
         
            }
            else
            {
                //if (ButtonPostLog.activeSelf == true)
                //{
                //    ButtonPostLog.active = false;
                //}
                UIManager _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                bestScoreUIString = www.downloadHandler.text;
                bool bestScoreIntSucces=Int32.TryParse(bestScoreUIString, out _uiManager.bestScoreUI);
                if (bestScoreIntSucces)
                {
                    ButtonPostLog.active = false;
                    TextBestScore.text = "Best Score: " + www.downloadHandler.text;
                }
                else
                {
                    ButtonPostLog.active = true;
                    TextButtonPostLog.text = www.downloadHandler.text;
                }
            }
        }
    }
    public void SetUpdateScore()
    {
        StartCoroutine(SetScoreDB(PlayerPrefs.GetString("UsernameLogin"), PlayerPrefs.GetString("UsernamePassword"), bestScore));
    }
    IEnumerator SetScoreDB(string username, string password, int bestScore)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);
        form.AddField("bestScore", bestScore);

        using (UnityWebRequest www = UnityWebRequest.Post("https://unityfsadsa.000webhostapp.com/gameUnity/SetScore.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                ButtonPostLog.active = true;
                TextButtonPostLog.text = www.error;
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                //if (ButtonPostLog.activeSelf==true)
                //{
                //    ButtonPostLog.active = false;
                //}
                //HolderConsoleDataManagerRegister.text = ;
            }
        }
    }
}
