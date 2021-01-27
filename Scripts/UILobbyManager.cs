using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILobbyManager : MonoBehaviour
{
    [SerializeField] private GameObject BoxLogin;
    [SerializeField] private GameObject BoxRegister;

    [SerializeField] private GameObject BoxResendActivateCode;
    [SerializeField] private Toggle BoxResendActivateCodeToogle;


    public void ShowAndHideLoginButton()
    {
        if (BoxLogin.activeSelf==false)
        {
            BoxLogin.active = true;
        }

        if (BoxRegister.activeSelf==true)
        {
            BoxRegister.active = false;
        }

        if (BoxResendActivateCode.activeSelf==true)
        {
            BoxResendActivateCode.active = false;
        }
    }
    public void ShowAndHideRegisterButton()
    {
        if (BoxRegister.activeSelf == false)
        {
            BoxRegister.active = true;
        }

        if (BoxLogin.activeSelf == true)
        {
            BoxLogin.active = false;
        }

        if (BoxResendActivateCode.activeSelf == true)
        {
            BoxResendActivateCode.active = false;
        }
    }
    public void ShowAndHideResendActivateCodeButton()
    {
        BoxResendActivateCodeToogle.isOn = false;

        if (BoxLogin.activeSelf==true)
        {
            BoxLogin.active = false;
        }

        if (BoxRegister.activeSelf == true)
        {
            BoxRegister.active = false;
        }

        if (BoxResendActivateCode.activeSelf==false)
        {
            BoxResendActivateCode.active = true;
        }
    }
    public void SceneManagerButtonMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

}
