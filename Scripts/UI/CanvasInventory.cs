using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CanvasInventory : MonoBehaviour
{
    public Button returnMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        returnMenu.onClick.AddListener(GoMainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GoMainMenu()
    {
        GameObject curMenu=GameManager.Instance.CanvasStack.Pop();
        curMenu.SetActive(false);
        GameManager.Instance.MainMenu.SetActive(true);
    }
}
