using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Canvas_maps : MonoBehaviour
{
    Button carparkBtn;
    Button returnBtn;
    public GameObject canvas_menu;
    private void Awake() 
    {
        carparkBtn=transform.GetChild(1).GetComponent<Button>();
        returnBtn=transform.GetChild(2).GetComponent<Button>();

        carparkBtn.onClick.AddListener(GoCarpark);
        returnBtn.onClick.AddListener(GoMainMenu);
    }
    void GoCarpark()
    {
        UnityEngine.Debug.Log("进入停车场地图");
        GameManager.Instance.sceneName="Carpark";
        StartCoroutine("LoadMaps");
    }
    void GoMainMenu()
    {
        GameObject curMenu=GameManager.Instance.CanvasStack.Pop();
        curMenu.SetActive(false);
        GameManager.Instance.MainMenu.SetActive(true);
    }
    private void Update() 
    {
        
    }
    IEnumerator LoadMaps()
    {
        GameManager.Instance.cleanStack();
        yield return SceneManager.LoadSceneAsync("Plane_Demo_Scene");
    }
}