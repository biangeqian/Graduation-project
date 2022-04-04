using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemForDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    private bool canDrag;
    private int size;
    private int indexD;
    public void OnBeginDrag(PointerEventData eventData)
    {
        //拖拽的格子有物体
        if(GetComponentInParent<ItemUI>().indexOfDataInBox!=-1)
        {
            canDrag=true;
            
            //记录原始数据
            indexD=GetComponentInParent<ItemUI>().indexOfDataInBox;
            size=InventoryManager.Instance.warehousePlayer.list[indexD].itemData.boxSize;

            if(size==1)
            {
                InventoryManager.Instance.warehouseContainer.itemUIs[indexD].indexOfDataInBox=-1;
            }
            else if(size==10)
            {
                InventoryManager.Instance.warehouseContainer.itemUIs[indexD].indexOfDataInBox=-1;
                InventoryManager.Instance.warehouseContainer.itemUIs[indexD+1].indexOfDataInBox=-1;
                InventoryManager.Instance.warehouseContainer.itemUIs[indexD+2].indexOfDataInBox=-1;
                InventoryManager.Instance.warehouseContainer.itemUIs[indexD+3].indexOfDataInBox=-1;
                InventoryManager.Instance.warehouseContainer.itemUIs[indexD+4].indexOfDataInBox=-1;
                InventoryManager.Instance.warehouseContainer.itemUIs[indexD+10].indexOfDataInBox=-1;
                InventoryManager.Instance.warehouseContainer.itemUIs[indexD+11].indexOfDataInBox=-1;
                InventoryManager.Instance.warehouseContainer.itemUIs[indexD+12].indexOfDataInBox=-1;
                InventoryManager.Instance.warehouseContainer.itemUIs[indexD+13].indexOfDataInBox=-1;
                InventoryManager.Instance.warehouseContainer.itemUIs[indexD+14].indexOfDataInBox=-1;
            }

            // InventoryManager.Instance.dragOrigItemUI=GetComponentInParent<ItemUI>();
            InventoryManager.Instance.dragOrigParent=(RectTransform)transform.parent;

            transform.SetParent(InventoryManager.Instance.dragArea.transform,true);
        }  
        else
        {
            canDrag=false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(canDrag)
        {
            //中心跟随鼠标位置
            if(size==1)
            {
                transform.position=new Vector2(eventData.position.x-32,eventData.position.y+32);   
            }
        }  
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(canDrag)
        {
            //是否指向UI物体
            if(InventoryManager.Instance.CheckInWarehouseUI(eventData.position))
            {
                ItemUI targetItemUI=eventData.pointerEnter.gameObject.GetComponentInParent<ItemUI>();
                if(targetItemUI)
                {
                    if(size==1)
                    {
                        if(check(targetItemUI.Index))
                        {
                            SwapWithTarget(targetItemUI.Index);
                            return;
                        }
                    }
                    else if(size==10)
                    {

                    }
                }
            }
            dragDefault();  
        }   
    }
    private bool check(int index)
    {
        if(index>=0&&index<260)
        {
            if(InventoryManager.Instance.warehouseContainer.itemUIs[index].indexOfDataInBox==-1)
            {
                return true;
            }
        }
        return false;
    }
    public void dragDefault()
    {
        transform.SetParent(InventoryManager.Instance.dragOrigParent,true);
        transform.position=InventoryManager.Instance.dragOrigParent.position;
    }
    public Item GetItemData(int index)
    {
        return InventoryManager.Instance.warehousePlayer.list[index];
    }
    public void SwapWithTarget(int index)
    {
        if(size==1) //TODO
        {
            //Data
            //交换目标格子在数据列表中的item数据
            InventoryManager.Instance.warehousePlayer.list[index]=GetItemData(InventoryManager.Instance.dragOrigItemUI.indexOfDataInBox);
            //交换图片位置
            //InventoryManager.Instance.warehousePlayer.list[index].imageboxIndex=index;
            //交换起始格子数据
            InventoryManager.Instance.warehousePlayer.list[InventoryManager.Instance.dragOrigItemUI.indexOfDataInBox]=null;
            //UI
            //交换位置
            InventoryManager.Instance.warehouseContainer.itemUIs[index].transform.GetChild(0).SetParent(InventoryManager.Instance.dragOrigParent,false);
            transform.SetParent(InventoryManager.Instance.warehouseContainer.itemUIs[index].transform,false);
            transform.position=InventoryManager.Instance.warehouseContainer.itemUIs[index].transform.position;
            //记录数据存放位置
            InventoryManager.Instance.warehouseContainer.itemUIs[index].indexOfDataInBox=index;
            InventoryManager.Instance.dragOrigItemUI.indexOfDataInBox=-1;
            //记录新的子物体
            InventoryManager.Instance.warehouseContainer.itemUIs[index].RefreshImageAndText();
            InventoryManager.Instance.dragOrigItemUI.RefreshImageAndText();
        }
        else if(size==10)
        {

        }
    }
}
