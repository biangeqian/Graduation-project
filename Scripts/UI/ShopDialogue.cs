using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDialogue : MonoBehaviour
{
    public GameObject Menu;
    private GameObject canvas_inventory;
    private Item currentShopItem;
    public Text itemName;
    public ItemUI itemUI;
    public Text description;
    public Text singleValue;
    public InputField inputField_number;
    private int goodsNum=1;
    private int shopModel=0;
    private int indexOfData;
    public Text totalValue;
    public Button button_buy;
    public Button button_sale;
    private void OnEnable() 
    {
        Menu.SetActive(false);
        canvas_inventory.SetActive(true);
        canvas_inventory.GetComponent<CanvasInventory>().setMarketModel();
    }
    private void OnDisable() 
    {
        Menu.SetActive(true);
        canvas_inventory.SetActive(false);
    }
    // Start is called before the first frame update
    void Awake()
    {
        canvas_inventory=GameManager.Instance.CanvasInventory;
        inputField_number.onValueChanged.AddListener(changeNumber); 
        button_buy.onClick.AddListener(buyGoods);
        button_sale.onClick.AddListener(saleGoods);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    public void setCurrentShopItem(Item item,int model,int index)
    {
        currentShopItem=item;
        shopModel=model;
        indexOfData=index;
        var itemSO=item.itemData;
        itemName.text=itemSO.descriptionName;
        
        int size=itemSO.boxSize;
        if(size==1) itemUI.image.rectTransform.sizeDelta = new Vector2(64, 64);
        else if(size==2) itemUI.image.rectTransform.sizeDelta = new Vector2(128, 64);
        else if(size==4) itemUI.image.rectTransform.sizeDelta = new Vector2(128, 128);
        else if(size==10) itemUI.image.rectTransform.sizeDelta = new Vector2(320, 128);

        itemUI.image.sprite=itemSO.image;
        itemUI.image.color=new Color(255,255,255,1);
        description.text=itemSO.descriptionTooltip;
        if(model==0) //买
        {
            singleValue.text=itemSO.maxValue.ToString();
            button_buy.gameObject.SetActive(true);
            button_sale.gameObject.SetActive(false);
        }
        else if(model==1) //卖
        {
            singleValue.text=(itemSO.maxValue*0.7).ToString();
            button_buy.gameObject.SetActive(false);
            button_sale.gameObject.SetActive(true);
        }
        changeNumber(goodsNum.ToString());
        totalValue.text=(int.Parse(singleValue.text)*goodsNum).ToString();
        
    }
    void changeNumber(string num)
    {
        if(shopModel==0)
        {
            if(num.Length>0)
            {
                if(num[0]=='-')
                {
                    num=num.Substring(1,num.Length-1);
                    inputField_number.text=num;
                    if(num!="") goodsNum=int.Parse(num);
                }
                else
                {
                    goodsNum=int.Parse(num);
                }
            } 
            else
            {
                goodsNum=1;
            }
            totalValue.text=(int.Parse(singleValue.text)*goodsNum).ToString();
        }
        else if(shopModel==1)
        {
            if(num.Length>0)
            {
                if(num[0]=='-')
                {
                    num=num.Substring(1,num.Length-1);
                    inputField_number.text=num;
                    if(num!="")
                    {
                        goodsNum=int.Parse(num);
                        if(goodsNum>currentShopItem.currentStack)
                        {
                            goodsNum=currentShopItem.currentStack;
                            inputField_number.text=goodsNum.ToString();
                        }
                    }
                }
                else
                {
                    goodsNum=int.Parse(num);
                    if(goodsNum>currentShopItem.currentStack)
                    {
                        goodsNum=currentShopItem.currentStack;
                        inputField_number.text=goodsNum.ToString();
                    }
                }
            } 
            else
            {
                goodsNum=1;
            }
            totalValue.text=(int.Parse(singleValue.text)*goodsNum).ToString();
        }
    }
    void buyGoods()
    {
        int cost=int.Parse(totalValue.text);
        if(goodsNum<=0||cost>GameManager.Instance.playerMoney) return;
        int buyDefaultNum=GameManager.Instance.CanvasInventory.GetComponent<InventoryManager>().warehouseContainer.
        AddItem(currentShopItem.itemData,goodsNum,currentShopItem.itemData.maxUse);
        GameManager.Instance.playerMoney-=(cost-buyDefaultNum*int.Parse(singleValue.text));
        GameManager.Instance.CanvasInventory.GetComponent<CanvasInventory>().Money.text="₽  "+GameManager.Instance.playerMoney.ToString();
    }
    void saleGoods()
    {
        int get=int.Parse(totalValue.text);
        GameManager.Instance.CanvasInventory.GetComponent<InventoryManager>().warehouseContainer.
        DeleteItem(indexOfData,goodsNum,GameManager.Instance.CanvasInventory.GetComponent<InventoryManager>().warehousePlayer);
        GameManager.Instance.playerMoney+=get;
        GameManager.Instance.CanvasInventory.GetComponent<CanvasInventory>().Money.text="₽  "+GameManager.Instance.playerMoney.ToString();
        button_sale.gameObject.SetActive(false);
    }
}
