using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerUI : MonoBehaviour
{
    public ItemUI[] itemUIs;
    public int rowNumber=10;
    public int totalNumber=260;
    //public ItemData_SO testData;//for test
    public InventoryData_SO marketData;
    public void RefreshUI()
    {
        for(int i = 0; i < itemUIs.Length; i++)
        {
            itemUIs[i].Index = i;
            
        }
    }
    public int AddItem(ItemData_SO itemData,int number,int useTimes)
    {
        //一格的物品
        if(itemData.boxSize==1)
        {
            for(int i=0;i<itemUIs.Length;i++)
            {
                if(check(i))
                {
                    UnityEngine.Debug.Log("在第"+i+"格添加1格物品");
                    
                    //UI
                    itemUIs[i].indexOfDataInBox=i;

                    itemUIs[i].image.rectTransform.sizeDelta = new Vector2(64, 64);
                    itemUIs[i].image.sprite=itemData.image;
                    itemUIs[i].image.color=new Color(255,255,255,1);
                    itemUIs[i].upText.text=itemData.descriptionName;
                    
                    if(itemData.isStackable)
                    {
                        //Data
                        InventoryManager.Instance.warehousePlayer.list[i]=new Item(itemData,number,useTimes);
                        itemUIs[i].bottomText.text=number.ToString();
                        return 0;
                    }
                    else
                    {
                        //Data
                        InventoryManager.Instance.warehousePlayer.list[i]=new Item(itemData,1,useTimes);
                        number--;
                        if(number<=0) break;
                    }
                }
            }
        }
        //10格的物品  步枪
        else if(itemData.boxSize==10)
        {
            for(int i=0;i<itemUIs.Length;i++)
            {
                if(i/rowNumber==(i+4)/rowNumber)
                {
                    if(check(i)&&check(i+1)&&check(i+2)&&check(i+3)&&check(i+4)
                    &&check(i+rowNumber)&&check(i+rowNumber+1)&&check(i+rowNumber+2)&&check(i+rowNumber+3)&&check(i+rowNumber+4))
                    {
                        UnityEngine.Debug.Log("在第"+i+"格添加10格物品");
                        //Data
                        InventoryManager.Instance.warehousePlayer.list[i]=new Item(itemData,1,useTimes);
                        //UI
                        itemUIs[i].indexOfDataInBox=i;
                        itemUIs[i+1].indexOfDataInBox=i;
                        itemUIs[i+2].indexOfDataInBox=i;
                        itemUIs[i+3].indexOfDataInBox=i;
                        itemUIs[i+4].indexOfDataInBox=i;
                        itemUIs[i+rowNumber].indexOfDataInBox=i;
                        itemUIs[i+rowNumber+1].indexOfDataInBox=i;
                        itemUIs[i+rowNumber+2].indexOfDataInBox=i;
                        itemUIs[i+rowNumber+3].indexOfDataInBox=i;
                        itemUIs[i+rowNumber+4].indexOfDataInBox=i;

                        itemUIs[i].image.rectTransform.sizeDelta = new Vector2(320, 128);
                        itemUIs[i].image.sprite=itemData.image;
                        itemUIs[i].image.color=new Color(255,255,255,1);
                        itemUIs[i].upText.text="";
                        number--;
                        if(number<=0) break;
                    }
                }
            }
        }
        else if(itemData.boxSize==4)
        {
            for(int i=0;i<itemUIs.Length;i++)
            {
                if(i/rowNumber==(i+1)/rowNumber)
                {
                    if(check(i)&&check(i+1)&&check(i+rowNumber)&&check(i+rowNumber+1))
                    {
                        UnityEngine.Debug.Log("在第"+i+"格添加4格物品");
                        //Data
                        InventoryManager.Instance.warehousePlayer.list[i]=new Item(itemData,1,useTimes);
                        //UI
                        itemUIs[i].indexOfDataInBox=i;
                        itemUIs[i+1].indexOfDataInBox=i;
                        itemUIs[i+rowNumber].indexOfDataInBox=i;
                        itemUIs[i+rowNumber+1].indexOfDataInBox=i;
                        
                        itemUIs[i].image.rectTransform.sizeDelta = new Vector2(128, 128);
                        itemUIs[i].image.sprite=itemData.image;
                        itemUIs[i].image.color=new Color(255,255,255,1);
                        itemUIs[i].upText.text="";
                        number--;
                        if(number<=0) break;
                    }
                }
            }
        }
        else if(itemData.boxSize==2)
        {
            for(int i=0;i<itemUIs.Length;i++)
            {
                if(i/rowNumber==(i+1)/rowNumber)
                {
                    if(check(i)&&check(i+1))
                    {
                        UnityEngine.Debug.Log("在第"+i+"格添加2格物品");
                        //Data
                        InventoryManager.Instance.warehousePlayer.list[i]=new Item(itemData,1,useTimes);
                        //UI
                        itemUIs[i].indexOfDataInBox=i;
                        itemUIs[i+1].indexOfDataInBox=i;

                        itemUIs[i].image.rectTransform.sizeDelta = new Vector2(128, 64);
                        itemUIs[i].image.sprite=itemData.image;
                        itemUIs[i].image.color=new Color(255,255,255,1);
                        itemUIs[i].upText.text="";
                        number--;
                        if(number<=0) break;
                    }
                }
            }
        }
        return number;
    }
    public void DeleteItem(int index,int number,InventoryData_SO data)
    {
        var item=data.list[index];
        if(item.itemData.boxSize==1)
        {
            if(item.currentStack-number>0)
            {
                item.currentStack-=number;
                itemUIs[index].bottomText.text=item.currentStack.ToString();
            }
            else
            {
                //Data
                data.list[index]=null;
                //UI
                itemUIs[index].indexOfDataInBox=-1;

                itemUIs[index].image.rectTransform.sizeDelta = new Vector2(64, 64);
                itemUIs[index].image.sprite=null;
                itemUIs[index].image.color=new Color(255,255,255,0);
                itemUIs[index].upText.text="";
                itemUIs[index].bottomText.text="";
            }
        }
        else if(item.itemData.boxSize==2)
        {
            //Data
            data.list[index]=null;
            //UI
            itemUIs[index].indexOfDataInBox=-1;
            itemUIs[index+1].indexOfDataInBox=-1;

            itemUIs[index].image.rectTransform.sizeDelta = new Vector2(64, 64);
            itemUIs[index].image.sprite=null;
            itemUIs[index].image.color=new Color(255,255,255,0);
            itemUIs[index].upText.text="";
        }
        else if(item.itemData.boxSize==4)
        {
            //Data
            data.list[index]=null;
            //UI
            itemUIs[index].indexOfDataInBox=-1;
            itemUIs[index+1].indexOfDataInBox=-1;
            itemUIs[index+rowNumber].indexOfDataInBox=-1;
            itemUIs[index+rowNumber+1].indexOfDataInBox=-1;

            itemUIs[index].image.rectTransform.sizeDelta = new Vector2(64, 64);
            itemUIs[index].image.sprite=null;
            itemUIs[index].image.color=new Color(255,255,255,0);
            itemUIs[index].upText.text="";
        }
        else if(item.itemData.boxSize==10)
        {
            //Data
            data.list[index]=null;
            //UI
            itemUIs[index].indexOfDataInBox=-1;
            itemUIs[index+1].indexOfDataInBox=-1;
            itemUIs[index+2].indexOfDataInBox=-1;
            itemUIs[index+3].indexOfDataInBox=-1;
            itemUIs[index+4].indexOfDataInBox=-1;
            itemUIs[index+rowNumber].indexOfDataInBox=-1;
            itemUIs[index+rowNumber+1].indexOfDataInBox=-1;
            itemUIs[index+rowNumber+2].indexOfDataInBox=-1;
            itemUIs[index+rowNumber+3].indexOfDataInBox=-1;
            itemUIs[index+rowNumber+4].indexOfDataInBox=-1;

            itemUIs[index].image.rectTransform.sizeDelta = new Vector2(64, 64);
            itemUIs[index].image.sprite=null;
            itemUIs[index].image.color=new Color(255,255,255,0);
            itemUIs[index].upText.text="";
        }
    }
    public void loadData(InventoryData_SO data)
    {
        //UnityEngine.Debug.Log(data.list.Count);
        for(int i=data.list.Count-1;i>=0;i--)
        {
            //list[i]被设置为null的时候访问list[i].itemData会报错
            if(data.list[i]!=null&&data.list[i].itemData!=null)
            {
                ItemData_SO itemData=data.list[i].itemData;
                if(itemData.boxSize==1)
                {
                    //UI
                    itemUIs[i].indexOfDataInBox=i;

                    itemUIs[i].image.rectTransform.sizeDelta = new Vector2(64, 64);
                    itemUIs[i].image.sprite=itemData.image;
                    itemUIs[i].image.color=new Color(255,255,255,1);
                    itemUIs[i].upText.text=itemData.descriptionName;
                    
                    if(data.list[i].currentStack>1)
                    {
                        itemUIs[i].bottomText.text=data.list[i].currentStack.ToString();
                    }
                }
                else if(itemData.boxSize==2)
                {
                    //UI
                    itemUIs[i].indexOfDataInBox=i;
                    itemUIs[i+1].indexOfDataInBox=i;

                    itemUIs[i].image.rectTransform.sizeDelta = new Vector2(128, 64);
                    itemUIs[i].image.sprite=itemData.image;
                    itemUIs[i].image.color=new Color(255,255,255,1);
                    itemUIs[i].upText.text="";
                }
                else if(itemData.boxSize==4)
                {
                    //UI
                    itemUIs[i].indexOfDataInBox=i;
                    itemUIs[i+1].indexOfDataInBox=i;
                    itemUIs[i+rowNumber].indexOfDataInBox=i;
                    itemUIs[i+rowNumber+1].indexOfDataInBox=i;
                    
                    itemUIs[i].image.rectTransform.sizeDelta = new Vector2(128, 128);
                    itemUIs[i].image.sprite=itemData.image;
                    itemUIs[i].image.color=new Color(255,255,255,1);
                    itemUIs[i].upText.text="";
                }
                else if(itemData.boxSize==10)
                {
                    //UI
                    itemUIs[i].indexOfDataInBox=i;
                    itemUIs[i+1].indexOfDataInBox=i;
                    itemUIs[i+2].indexOfDataInBox=i;
                    itemUIs[i+3].indexOfDataInBox=i;
                    itemUIs[i+4].indexOfDataInBox=i;
                    itemUIs[i+rowNumber].indexOfDataInBox=i;
                    itemUIs[i+rowNumber+1].indexOfDataInBox=i;
                    itemUIs[i+rowNumber+2].indexOfDataInBox=i;
                    itemUIs[i+rowNumber+3].indexOfDataInBox=i;
                    itemUIs[i+rowNumber+4].indexOfDataInBox=i;

                    itemUIs[i].image.rectTransform.sizeDelta = new Vector2(320, 128);
                    itemUIs[i].image.sprite=itemData.image;
                    itemUIs[i].image.color=new Color(255,255,255,1);
                    itemUIs[i].upText.text="";
                }
            }
            else
            {
                //UI
                itemUIs[i].indexOfDataInBox=-1;
                itemUIs[i].image.rectTransform.sizeDelta = new Vector2(64, 64);
                itemUIs[i].image.sprite=null;
                itemUIs[i].image.color=new Color(255,255,255,0);
                itemUIs[i].upText.text="";
                itemUIs[i].bottomText.text="";
            }
        }
    }
    private bool check(int index)
    {
        if(index>=0&&index<totalNumber)
        {
            if(itemUIs[index].indexOfDataInBox==-1)
            {
                return true;
            }
        }
        return false;
    }
    private void Start() 
    {
        RefreshUI();
        if(marketData)
        {
            loadData(marketData);
        }
    }
    private void Update() 
    {
        //for test
        // if(Input.GetKeyDown(KeyCode.K))
        // {
        //     AddItem(testData,1,-1);
        // }
    }
}
