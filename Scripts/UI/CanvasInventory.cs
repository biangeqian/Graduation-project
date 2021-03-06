using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CanvasInventory : MonoBehaviour
{
    public Text Money;
    public Button returnMenu;
    public GameObject inventory;
    public GameObject bag;
    public GameObject safeBag;
    public GameObject dropBox;
    public GameObject equipment;
    private bool isReturnMainMenu = true;
    private void OnEnable() 
    {
        inventory.SetActive(true);
        bag.SetActive(true);
        safeBag.SetActive(true);
        equipment.SetActive(true);
        dropBox.SetActive(false);
        returnMenu.gameObject.SetActive(true);
        isReturnMainMenu = true;
        Money.text="₽  "+GameManager.Instance.playerMoney.ToString();
    }
    public void setMarketModel()
    {
        bag.SetActive(false);
        safeBag.SetActive(false);
        equipment.SetActive(false);
        isReturnMainMenu = false;
    }
    public void setBattleModel()
    {
        inventory.SetActive(false);
        isReturnMainMenu = false;
    }
    public void setDropBoxModel()
    {
        inventory.SetActive(false);
        dropBox.SetActive(true);
        isReturnMainMenu = false;
    }
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
        if(isReturnMainMenu)
        {
            GameManager.Instance.MainMenu.SetActive(true);
        }
    }
    public void updateMoney()
    {
        Money.text="₽  "+GameManager.Instance.playerMoney.ToString();
    }
}
