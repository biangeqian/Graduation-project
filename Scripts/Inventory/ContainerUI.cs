using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerUI : MonoBehaviour
{
    public ItemUI[] itemUIs;
    public ItemData_SO testData;//for test
    public void RefreshUI()
    {
        for(int i = 0; i < itemUIs.Length; i++)
        {
            itemUIs[i].Index = i;
            
        }
    }
    public void AddItem(Item item)
    {
        //一格的物品
        if(item.itemData.boxSize==1)
        {
            for(int i=0;i<itemUIs.Length;i++)
            {
                if(itemUIs[i].indexOfDataInBox==-1)
                {
                    UnityEngine.Debug.Log("在第"+i+"格添加物品");
                    //Data
                    //item根格子序号
                    item.imageboxIndex=i;
                    InventoryManager.Instance.warehousePlayer.list[i]=item;
                    //UI
                    //图片根格子序号
                    itemUIs[i].indexOfDataInBox=i;
                    itemUIs[i].image.sprite=item.itemData.image;
                    itemUIs[i].image.color=new Color(255,255,255,1);
                    itemUIs[i].upText.text=item.itemData.descriptionName;
                    if(item.currentStack>1)
                    {
                        itemUIs[i].bottomText.text=(item.currentStack).ToString();
                    }
                    break;
                }
            }
        }
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
            AddItem(new Item(testData,1,1));

        }
        else if(Input.GetKeyDown(KeyCode.L))
        {
            AddItem(new Item(testData,3,1));

        }
    }
}
