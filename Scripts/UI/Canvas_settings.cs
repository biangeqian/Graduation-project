using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_settings : MonoBehaviour
{
    public AudioSource audioSource;
    public Button returnBtn;
    public Slider slider;
    public Text volumeScore;
    public GameObject canvas_menu;
    private void Awake() 
    {
        returnBtn.onClick.AddListener(GoMainMenu);
    }
    // Update is called once per frame
    void Update()
    {
        audioSource.volume=slider.value;
        volumeScore.text=(Mathf.Round(slider.value*100)).ToString();
    }
    void GoMainMenu()
    {
        canvas_menu.SetActive(true);
        gameObject.SetActive(false);
    }
}
