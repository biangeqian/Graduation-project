using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerUI : MonoBehaviour
{
    public ItemUI[] itemUIs;
    private int rowNumber=10;
    public ItemData_SO testData;//for test
    public void RefreshUI()
    {
        for(int i = 0; i < itemUIs.Length; i++)
        {
            itemUIs[i].Index = i;
            
        }
    }
    public void AddItem(ItemData_SO itemData,int number,int useTimes)
    {
        //一格的物品
        if(itemData.boxSize==1)
        {
            for(int i=0;i<itemUIs.Length;i++)
            {
                if(check(i))
                {
                    UnityEngine.Debug.Log("在第"+i+"格添加1格物品");
                    //Data
                    InventoryManager.Instance.warehousePlayer.list[i]=new Item(itemData,number,useTimes);
                    //UI
                    itemUIs[i].indexOfDataInBox=i;
                    itemUIs[i].image.sprite=itemData.image;
                    itemUIs[i].image.color=new Color(255,255,255,1);
                    itemUIs[i].upText.text=itemData.descriptionName;
                    if(number>1)
                    {
                        itemUIs[i].bottomText.text=number.ToString();
                    }
                    break;
                }
            }
        }
        //10格的物品  步枪
        else if(itemData.boxSize==10)
        {
            for(int i=0;i<itemUIs.Length;i++)
            {
                if(i/10==(i+4)/10)
                {
                    if(check(i)&&check(i+1)&&check(i+2)&&check(i+3)&&check(i+4)
                    &&check(i+10)&&check(i+11)&&check(i+12)&&check(i+13)&&check(i+14))
                    {
                        UnityEngine.Debug.Log("在第"+i+"格添加10格物品");
                        //Data
                        InventoryManager.Instance.warehousePlayer.list[i]=new Item(itemData,number,useTimes);
                        //UI
                        itemUIs[i].indexOfDataInBox=i;
                        itemUIs[i+1].indexOfDataInBox=i;
                        itemUIs[i+2].indexOfDataInBox=i;
                        itemUIs[i+3].indexOfDataInBox=i;
                        itemUIs[i+4].indexOfDataInBox=i;
                        itemUIs[i+10].indexOfDataInBox=i;
                        itemUIs[i+11].indexOfDataInBox=i;
                        itemUIs[i+12].indexOfDataInBox=i;
                        itemUIs[i+13].indexOfDataInBox=i;
                        itemUIs[i+14].indexOfDataInBox=i;

                        itemUIs[i].image.rectTransform.sizeDelta = new Vector2(320, 128);
                        itemUIs[i].image.sprite=itemData.image;
                        itemUIs[i].image.color=new Color(255,255,255,1);
                        itemUIs[i].upText.text="";
                        break;
                    }
                }
            }
        }
    }
    private bool check(int index)
    {
        if(index>=0&&index<260)
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
    }
    private void Update() 
    {
        //for test
        if(Input.GetKeyDown(KeyCode.K))
        {
            AddItem(testData,1,-1);

        }
        // else if(Input.GetKeyDown(KeyCode.L))
        // {
        //     AddItem(new Item(testData,1,-1));

        // }
    }
}
