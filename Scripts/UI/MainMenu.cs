using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    Button startBtn;
    Button houseBtn;
    Button marketBtn;
    Button shootBtn;
    Button settingBtn;
    Button escapeBtn;
    public GameObject canvas_maps;
    public GameObject canvas_settings;
    private void Awake() 
    {
        startBtn=transform.GetChild(1).GetComponent<Button>();
        houseBtn=transform.GetChild(2).GetComponent<Button>();
        marketBtn=transform.GetChild(3).GetComponent<Button>();
        shootBtn=transform.GetChild(4).GetComponent<Button>();
        settingBtn=transform.GetChild(5).GetComponent<Button>();
        escapeBtn=transform.GetChild(6).GetComponent<Button>();

        startBtn.onClick.AddListener(StartGame);
        houseBtn.onClick.AddListener(GoHouse);
        marketBtn.onClick.AddListener(GoMarket);
        shootBtn.onClick.AddListener(GoShooting);
        settingBtn.onClick.AddListener(GoSetting);
        escapeBtn.onClick.AddListener(Quit);
    }
    void StartGame()
    {
        UnityEngine.Debug.Log("开始游戏");
        canvas_maps.SetActive(true);
        gameObject.SetActive(false);
    }
    void GoHouse()
    {
        UnityEngine.Debug.Log("打开仓库");
    }
    void GoMarket()
    {
        UnityEngine.Debug.Log("打开市场");
    }
    void GoShooting()
    {
        UnityEngine.Debug.Log("打开靶场");
    }
    void GoSetting()
    {
        UnityEngine.Debug.Log("打开设置");
        canvas_settings.SetActive(true);
        gameObject.SetActive(false);
    }
    void Quit()
    {
        UnityEngine.Debug.Log("退出游戏");
        //编辑器模式下不生效
        Application.Quit();
    }
}
