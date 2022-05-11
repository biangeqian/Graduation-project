using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Canvas_death : MonoBehaviour
{
    public Button btn_goMainMenu;
    public Button btn_quit;
    // Start is called before the first frame update
    void Start()
    {
        btn_goMainMenu.onClick.AddListener(goMainMenu);
        btn_quit.onClick.AddListener(quit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void goMainMenu()
    {
        GameManager.Instance.CanvasStack.Pop();//这里用clean会报错，原因未知
        SceneManager.LoadSceneAsync("Menu");
    }
    void quit()
    {
        UnityEngine.Debug.Log("退出游戏");
        //编辑器模式下不生效
        Application.Quit();
    }
}
