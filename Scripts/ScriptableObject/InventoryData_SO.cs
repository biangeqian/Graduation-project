using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FPS/InventoryData")]
public class InventoryData_SO : ScriptableObject
{
    public enum Owned{Bag,Inventory,Market,DropBox};
    public Owned owned;
    public List<Item> list=new List<Item>();
    
}
[System.Serializable]
public class Item
{
    public Item(ItemData_SO data_SO,int number,int useTimes)
    {
        this.itemData=data_SO;
        if(data_SO.isStackable)
        {
            this.currentStack=number;
        }
        else
        {
            this.currentStack=1;
        }
        this.currentUseTimes=useTimes;
        //有耐久的不可堆叠
        this.currentValue=(int)Mathf.Ceil(((float)useTimes/data_SO.maxUse)*data_SO.maxValue);
        this.currentWeight=this.currentStack*data_SO.weightOfOne;

        
    }
    public ItemData_SO itemData;
    public int currentStack;
    public int currentValue;
    public int currentUseTimes;
    public float currentWeight;
    //public int imageboxIndex;
    //public int rotateState;
}
