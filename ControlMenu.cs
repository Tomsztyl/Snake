using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlMenu : MonoBehaviour
{
    [SerializeField]
    private AudioSource _menuSound;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private Slider _gameSlider;
    [SerializeField]
    private Slider _menuSlider;
    [SerializeField]
    private bool OptionButtonClick = false;
    [SerializeField]
    private GameObject _optionsMenuButton;
    private void Start()
    {
        _gameSlider.value = PlayerPrefs.GetFloat("SoundGame");
        _menuSlider.value = PlayerPrefs.GetFloat("SoundMenu");
    }
    public void NewGameButton()
    {
        _audioSource.Play();
        SceneManager.LoadScene("GameScene");
    }
    public void OptionsButton()
    {
        _audioSource.Play();
        if (OptionButtonClick==false)
        {
            _optionsMenuButton.active = true;
            OptionButtonClick = true;
        }
        else
        {
            _optionsMenuButton.active = false;
            OptionButtonClick = false;
        }
        //Instantiate(_prefabsOptionsMenuButton, new Vector3(-223, 0, 0), Quaternion.identity);
    }
    public void ExitButton()
    {
        _audioSource.Play();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
    public void MenuSlider()
    {
        _menuSound.volume = _menuSlider.value;
        PlayerPrefs.SetFloat("SoundMenu", _menuSlider.value);
    }
    public void GameSlider()
    {
        PlayerPrefs.SetFloat("SoundGame", _gameSlider.value);
        //Debug.Log(PlayerPrefs.GetFloat("SoundGame"));
    }
}
