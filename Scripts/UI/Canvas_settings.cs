using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Canvas_settings : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider slider;
    public Text volumeScore;
    public Button GoMenuBtn;
    public Button QuitBtn;

    void Awake() 
    {
        GoMenuBtn.onClick.AddListener(goMenu);
        QuitBtn.onClick.AddListener(quit);
    }
    private void OnEnable() 
    {
        if(SceneManager.GetActiveScene ().name=="Menu")
        {
            GoMenuBtn.gameObject.SetActive(false);
            QuitBtn.gameObject.SetActive(false);
        }
        else
        {
            GoMenuBtn.gameObject.SetActive(true);
            QuitBtn.gameObject.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(audioSource)
        {
            audioSource.volume=slider.value;
        }
        volumeScore.text=(Mathf.Round(slider.value*100)).ToString();
    }
    void goMenu()
    {
        GameManager.Instance.cleanStack();
        SceneManager.LoadSceneAsync("Menu");
    }
    void quit() 
    {
        Application.Quit();
    }
}
