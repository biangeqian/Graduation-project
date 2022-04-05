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
    private Transform dragItem;
    public void OnBeginDrag(PointerEventData eventData)
    {
        //拖拽的格子有物体
        if(GetComponentInParent<ItemUI>().indexOfDataInBox!=-1)
        {
            canDrag=true;
            
            //记录原始数据
            indexD=GetComponentInParent<ItemUI>().indexOfDataInBox;
            size=InventoryManager.Instance.warehousePlayer.list[indexD].itemData.boxSize;
            dragItem=InventoryManager.Instance.warehouseContainer.itemUIs[indexD].transform.GetChild(0);

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

            InventoryManager.Instance.dragOrigParent=(RectTransform)dragItem.parent;

            dragItem.SetParent(InventoryManager.Instance.dragArea.transform,true);
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
            dragItem.position=new Vector2(eventData.position.x-32,eventData.position.y+32);   
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
                        if(targetItemUI.Index/10==(targetItemUI.Index+4)/10)
                        {
                            if(check(targetItemUI.Index)&&check(targetItemUI.Index+1)&&check(targetItemUI.Index+2)&&check(targetItemUI.Index+3)&&check(targetItemUI.Index+4)
                            &&check(targetItemUI.Index+10)&&check(targetItemUI.Index+11)&&check(targetItemUI.Index+12)&&check(targetItemUI.Index+13)&&check(targetItemUI.Index+14))
                            {
                                SwapWithTarget(targetItemUI.Index);
                                return;
                            }
                        }
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
    private void dragDefault()
    {
        dragItem.SetParent(InventoryManager.Instance.dragOrigParent,true);
        dragItem.position=InventoryManager.Instance.dragOrigParent.position;
        if(size==1)
        {
            InventoryManager.Instance.warehouseContainer.itemUIs[indexD].indexOfDataInBox=indexD;
        }
        else if(size==10)
        {
            InventoryManager.Instance.warehouseContainer.itemUIs[indexD].indexOfDataInBox=indexD;
            InventoryManager.Instance.warehouseContainer.itemUIs[indexD+1].indexOfDataInBox=indexD;
            InventoryManager.Instance.warehouseContainer.itemUIs[indexD+2].indexOfDataInBox=indexD;
            InventoryManager.Instance.warehouseContainer.itemUIs[indexD+3].indexOfDataInBox=indexD;
            InventoryManager.Instance.warehouseContainer.itemUIs[indexD+4].indexOfDataInBox=indexD;
            InventoryManager.Instance.warehouseContainer.itemUIs[indexD+10].indexOfDataInBox=indexD;
            InventoryManager.Instance.warehouseContainer.itemUIs[indexD+11].indexOfDataInBox=indexD;
            InventoryManager.Instance.warehouseContainer.itemUIs[indexD+12].indexOfDataInBox=indexD;
            InventoryManager.Instance.warehouseContainer.itemUIs[indexD+13].indexOfDataInBox=indexD;
            InventoryManager.Instance.warehouseContainer.itemUIs[indexD+14].indexOfDataInBox=indexD;
        }
    }
    private Item GetItemData(int index)
    {
        return InventoryManager.Instance.warehousePlayer.list[index];
    }
    private void SwapWithTarget(int index)
    {
        if(size==1)
        {
            //Data
            //交换目标格子在数据列表中的item数据
            InventoryManager.Instance.warehousePlayer.list[index]=GetItemData(indexD);
            InventoryManager.Instance.warehousePlayer.list[indexD]=null;
            //UI交换位置
            InventoryManager.Instance.warehouseContainer.itemUIs[index].transform.GetChild(0).SetParent(InventoryManager.Instance.dragOrigParent,false);
            dragItem.SetParent(InventoryManager.Instance.warehouseContainer.itemUIs[index].transform,false);
            dragItem.position=InventoryManager.Instance.warehouseContainer.itemUIs[index].transform.position;
            //记录数据存放位置
            InventoryManager.Instance.warehouseContainer.itemUIs[index].indexOfDataInBox=index;
            //刷新子物体
            InventoryManager.Instance.warehouseContainer.itemUIs[index].RefreshImageAndText();
            InventoryManager.Instance.warehouseContainer.itemUIs[indexD].RefreshImageAndText();
        }
        else if(size==10)
        {
            //Data
            //交换目标格子在数据列表中的item数据
            InventoryManager.Instance.warehousePlayer.list[index]=GetItemData(indexD);
            InventoryManager.Instance.warehousePlayer.list[indexD]=null;
            //UI交换位置
            InventoryManager.Instance.warehouseContainer.itemUIs[index].transform.GetChild(0).SetParent(InventoryManager.Instance.dragOrigParent,false);
            dragItem.SetParent(InventoryManager.Instance.warehouseContainer.itemUIs[index].transform,false);
            dragItem.position=InventoryManager.Instance.warehouseContainer.itemUIs[index].transform.position;
            //记录数据存放位置
            InventoryManager.Instance.warehouseContainer.itemUIs[index].indexOfDataInBox=index;
            InventoryManager.Instance.warehouseContainer.itemUIs[index+1].indexOfDataInBox=index;
            InventoryManager.Instance.warehouseContainer.itemUIs[index+2].indexOfDataInBox=index;
            InventoryManager.Instance.warehouseContainer.itemUIs[index+3].indexOfDataInBox=index;
            InventoryManager.Instance.warehouseContainer.itemUIs[index+4].indexOfDataInBox=index;
            InventoryManager.Instance.warehouseContainer.itemUIs[index+10].indexOfDataInBox=index;
            InventoryManager.Instance.warehouseContainer.itemUIs[index+11].indexOfDataInBox=index;
            InventoryManager.Instance.warehouseContainer.itemUIs[index+12].indexOfDataInBox=index;
            InventoryManager.Instance.warehouseContainer.itemUIs[index+13].indexOfDataInBox=index;
            InventoryManager.Instance.warehouseContainer.itemUIs[index+14].indexOfDataInBox=index;
            //刷新子物体
            InventoryManager.Instance.warehouseContainer.itemUIs[index].RefreshImageAndText();
            InventoryManager.Instance.warehouseContainer.itemUIs[indexD].RefreshImageAndText();
        }
    }
}
