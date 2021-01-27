using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyDataBase : MonoBehaviour
{
    [SerializeField] private Text HolderConsoleDataManagerLogin;
    [SerializeField] private Text HolderConsoleDataManagerRegister;
    [SerializeField] private Text HolderConsoleDataManagerResend;

    [SerializeField] private InputField TextLogin;
    [SerializeField] private InputField TextPassword;

    [SerializeField] private InputField TextLoginReg;
    [SerializeField] private InputField TextPasswordReg;
    [SerializeField] private InputField TextMailReg;
    [SerializeField] private InputField TextActivateCode;

    [SerializeField] private InputField TextMailResend;
    [SerializeField] private InputField TextPasswordResend;

    [SerializeField] private Text TextUserButton;

    [SerializeField] private GameObject ControllsLogin;

    [SerializeField] private GameObject ButtonMenu;
    [SerializeField] private GameObject InputActivateCode;
    [SerializeField] private GameObject ToggleResendCode;

    [SerializeField] public ControllsLogin _controllsLogin;


    private void ChceckInputFieldIsEmptyLogin()
    {
        if (InputActivateCode.activeSelf!=true)
        {
            if (string.IsNullOrEmpty(TextLogin.text) || string.IsNullOrEmpty(TextPassword.text))
            {
                //Empty string
                //Debug.Log("String is Empty Login");
                HolderConsoleDataManagerLogin.text = "Complete all windows";
            }
            else
            {
                StartCoroutine(Login(TextLogin.text, TextPassword.text));
            }
        }
        else
        {
            if (string.IsNullOrEmpty(TextLogin.text) || string.IsNullOrEmpty(TextPassword.text) || string.IsNullOrEmpty(TextActivateCode.text))
            {
                //Empty string
                //Debug.Log("String is Empty Login");
                HolderConsoleDataManagerLogin.text = "Complete all windows";
            }
            else
            {
                StartCoroutine(LoginActivateCode(TextLogin.text, TextPassword.text,TextActivateCode.text));
            }
        }

    }
    private void ChceckInputFieldIsEmptyRegister()
    {
        if (string.IsNullOrEmpty(TextLoginReg.text)||string.IsNullOrEmpty(TextPasswordReg.text)||string.IsNullOrEmpty(TextMailReg.text))
        {
            //Empty string
            //Debug.Log("String is Empty Register");
            HolderConsoleDataManagerRegister.text = "Complete all windows";
        }
        else
        {
            StartCoroutine(Register(TextLoginReg.text, TextPasswordReg.text, TextMailReg.text));
        }
    }
    private void ChceckInputFieldIsEmptyResend()
    {
        if (string.IsNullOrEmpty(TextMailResend.text) || string.IsNullOrEmpty(TextPasswordResend.text))
        {
            //Empty string
            //Debug.Log("String is Empty Register");
            HolderConsoleDataManagerResend.text = "Complete all windows";
        }
        else
        {
            //Debug.Log("Welcome");
            StartCoroutine(LoginResendActivateCode(TextMailResend.text, TextPasswordResend.text));
        }
    }

    public void ExeciutyLogin()
    {
        HolderConsoleDataManagerLogin.text = "";
        ChceckInputFieldIsEmptyLogin();
    }
    public void ExeciutyRegister()
    {
        HolderConsoleDataManagerRegister.text = "";
        ChceckInputFieldIsEmptyRegister();
    }
    public void ExeciutyResendActivateCode()
    {
        HolderConsoleDataManagerResend.text = "";
        ChceckInputFieldIsEmptyResend();
    }
    IEnumerator LoginResendActivateCode(string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginMail", email);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("https://unityfsadsa.000webhostapp.com/gameUnity/ResendActivateCode.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                HolderConsoleDataManagerResend.text = www.error;
            }
            else
            {
                HolderConsoleDataManagerResend.text = www.downloadHandler.text;
            }
        }
    }
    IEnumerator LoginActivateCode(string username, string password,string activatecode)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);
        form.AddField("loginActivateCode", activatecode);
        //Debug.Log("LoginActivateCode : " + activatecode);

        using (UnityWebRequest www = UnityWebRequest.Post("https://unityfsadsa.000webhostapp.com/gameUnity/ActivateCodeManager.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                HolderConsoleDataManagerLogin.text = www.error;
            }
            else
            {
                if (www.downloadHandler.text == "Login Success")
                {
                    ControllsLogin.active = true;
                    //PlayerPrefs.SetString("ControllLogin", "true");
                    _controllsLogin = GameObject.FindWithTag("ControllsLogin").GetComponent<ControllsLogin>();
                    ButtonMenu.active = true;
                    TextUserButton.text = username;
                    PlayerPrefs.SetString("UsernameLogin", username);
                    PlayerPrefs.SetString("UsernamePassword", password);
                    //Debug.Log("Login User: " + PlayerPrefs.GetString("UsernameLogin"));
                }
                else if (www.downloadHandler.text == "Input activate code from email")
                {
                    InputActivateCode.active = true;
                    Debug.Log("Welcome");
                    ToggleResendCode.active = true;
                }
                HolderConsoleDataManagerLogin.text = www.downloadHandler.text;
            }
        }
    }
    IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);       

        using (UnityWebRequest www = UnityWebRequest.Post("https://unityfsadsa.000webhostapp.com/gameUnity/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                HolderConsoleDataManagerLogin.text = www.error;
            }
            else
            {
                if (www.downloadHandler.text == "Login Success")
                {
                    ControllsLogin.active = true;
                    //PlayerPrefs.SetString("ControllLogin", "true");
                    _controllsLogin = GameObject.FindWithTag("ControllsLogin").GetComponent<ControllsLogin>();
                    ButtonMenu.active = true;
                    TextUserButton.text = username;
                    PlayerPrefs.SetString("UsernameLogin", username);
                    PlayerPrefs.SetString("UsernamePassword", password);
                    //Debug.Log("Login User: " + PlayerPrefs.GetString("UsernameLogin"));
                }
                else if (www.downloadHandler.text== "Input activate code from email")
                {
                    InputActivateCode.active = true;
                    ToggleResendCode.active = true;
                }
                HolderConsoleDataManagerLogin.text = www.downloadHandler.text;
            }
        }
    }
    public void LogoutUserAndHideControllsLogin()
    {
        ButtonMenu.active = false;
        TextUserButton.text ="";
        PlayerPrefs.SetString("UsernameLogin","");
        PlayerPrefs.SetString("UsernamePassword","");
        //Debug.Log("Logout User: " + PlayerPrefs.GetString("UsernameLogin"));
        HolderConsoleDataManagerLogin.text = "Logout";
        //PlayerPrefs.SetString("ControllLogin", "false");
        ControllsLogin.active = false;

    }
    IEnumerator Register(string usernameReg, string passwordReg, string mailReg)
    {
        WWWForm form = new WWWForm();
        form.AddField("registerUser", usernameReg);
        form.AddField("registerPass", passwordReg);
        form.AddField("registerMail", mailReg);

        using (UnityWebRequest www = UnityWebRequest.Post("https://unityfsadsa.000webhostapp.com/gameUnity/Register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                HolderConsoleDataManagerRegister.text = www.error;
            }
            else
            {
                HolderConsoleDataManagerRegister.text = www.downloadHandler.text;
            }
        }
    }
}

