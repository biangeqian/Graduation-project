using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public string sceneName="Menu";
    public GameObject SettingUI;

    public GameObject MainMenu;
    public GameObject HitFeedbackUI;
    public GameObject Player;
    public Stack<GameObject> CanvasStack=new Stack<GameObject>();

    void Update() 
    {
        if(SceneManager.GetActiveScene().name=="Menu"||SceneManager.GetActiveScene().name=="Plane_Demo_Scene")
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            if(CanvasStack.Count>0)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape)&&SceneManager.GetActiveScene().name!="Plane_Demo_Scene")
        {
            if(CanvasStack.Count==0)
            {
                SettingUI.SetActive(true);
                CanvasStack.Push(SettingUI);
            }
            else
            {
                GameObject topCanvas=CanvasStack.Pop();
                topCanvas.SetActive(false);
                if(SceneManager.GetActiveScene().name=="Menu"&&CanvasStack.Count==0)
                {
                    MainMenu.SetActive(true);
                }
            }
            
        }
    }
    
}
