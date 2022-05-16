using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasMarket : MonoBehaviour
{
    public Button Mechanic;
    public Button Skier;
    public Button Therapist;
    public Button returnMenu;
    public GameObject MechanicShop;
    [Header("Task")]
    public Text task1Now;
    public Text task2Now;
    public Button btn_submit1;
    public Button btn_submit2;
    public GameObject tips;
    private int HpBaoNumber;
    // Start is called before the first frame update
    void Start()
    {
        Mechanic.onClick.AddListener(goMechanic);
        Skier.onClick.AddListener(goSkier);
        Therapist.onClick.AddListener(goTherapist);
        returnMenu.onClick.AddListener(GoMainMenu);
        btn_submit1.onClick.AddListener(submit1);
        btn_submit2.onClick.AddListener(submit2);
    }
    private void OnEnable() 
    {
        HpBaoNumber=0;
        var list=GameManager.Instance.CanvasInventory.GetComponent<InventoryManager>().warehousePlayer.list;
        for(int i=0;i<list.Count;i++)
        {
            if(list[i]!=null&&list[i].itemData!=null&&list[i].itemData.itemName=="bigHP")
            {
                HpBaoNumber++;
            }
        }
        task1Now.text="当前  "+HpBaoNumber.ToString()+"/2";

        task2Now.text="当前  "+GameManager.Instance.taskData.killFashiNumber.ToString()+"/3";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void goMechanic()
    {
        MechanicShop.SetActive(true);
        GameManager.Instance.CanvasStack.Push(MechanicShop);
    }
    void goSkier()
    {
        
    }
    void goTherapist()
    {

    }
    void GoMainMenu()
    {
        GameObject curMenu=GameManager.Instance.CanvasStack.Pop();
        curMenu.SetActive(false);
        GameManager.Instance.MainMenu.SetActive(true);
    }
    void submit1()
    {
        if(HpBaoNumber>=2)
        {
            var list=GameManager.Instance.CanvasInventory.GetComponent<InventoryManager>().warehousePlayer.list;
            var container=GameManager.Instance.CanvasInventory.GetComponent<InventoryManager>().warehouseContainer;
            int need=2;
            for(int i=0;i<list.Count;i++)
            {
                if(list[i]!=null&&list[i].itemData!=null&&list[i].itemData.itemName=="bigHP")
                {
                    if(need<=0) break;
                    need--;
                    container.DeleteItem(i,1,GameManager.Instance.CanvasInventory.GetComponent<InventoryManager>().warehousePlayer);
                }
            }
            var reward=GameManager.Instance.taskData.reward1;
            GameManager.Instance.playerMoney+=reward.money;
            GameManager.Instance.CanvasInventory.GetComponent<CanvasInventory>().Money.text="₽  "+GameManager.Instance.playerMoney.ToString();
            for(int i=0;i<reward.items.Count;i++)
            {
                if(reward.items[i].itemData.isStackable)
                {
                    container.AddItem(reward.items[i].itemData,reward.items[i].currentStack,reward.items[i].itemData.maxUse);
                }
                else
                {
                    for(int j=0;j<reward.items[i].currentStack;j++)
                    {
                        container.AddItem(reward.items[i].itemData,1,reward.items[i].itemData.maxUse);
                    }
                }
            }
            HpBaoNumber-=2;
            task1Now.text="当前  "+HpBaoNumber.ToString()+"/2";
            tips.GetComponent<TipsText>().setText("奖励已领取至仓库");
        }
        else
        {
            tips.GetComponent<TipsText>().setText("物品不足");
        }
    }
    void submit2()
    {
        if(GameManager.Instance.taskData.killFashiNumber>=3)
        {
            var container=GameManager.Instance.CanvasInventory.GetComponent<InventoryManager>().warehouseContainer;
            var reward=GameManager.Instance.taskData.reward2;
            GameManager.Instance.playerMoney+=reward.money;
            GameManager.Instance.CanvasInventory.GetComponent<CanvasInventory>().Money.text="₽  "+GameManager.Instance.playerMoney.ToString();
            for(int i=0;i<reward.items.Count;i++)
            {
                if(reward.items[i].itemData.isStackable)
                {
                    container.AddItem(reward.items[i].itemData,reward.items[i].currentStack,reward.items[i].itemData.maxUse);
                }
                else
                {
                    for(int j=0;j<reward.items[i].currentStack;j++)
                    {
                        container.AddItem(reward.items[i].itemData,1,reward.items[i].itemData.maxUse);
                    }
                }
            }
            GameManager.Instance.taskData.killFashiNumber-=3;
            task2Now.text="当前  "+GameManager.Instance.taskData.killFashiNumber.ToString()+"/3";
            tips.GetComponent<TipsText>().setText("奖励已领取至仓库");
        }
        else
        {
            tips.GetComponent<TipsText>().setText("击杀数量不足");
        }
    }
}
