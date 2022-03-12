using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemForDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    private bool canDrag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        //拖拽的格子有物体
        if(GetComponentInParent<ItemUI>().indexOfDataInBox!=-1)
        {
            canDrag=true;
            
            //记录原始数据
            InventoryManager.Instance.dragOrigItemUI=GetComponentInParent<ItemUI>();
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
            transform.position=new Vector2(eventData.position.x-32,eventData.position.y+32);   
        }  
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(canDrag)
        {
            //是否指向UI物体
            if(InventoryManager.Instance.CheckInWarehouseUI(eventData.position))
            {
                ItemUI tmpItemUI=eventData.pointerEnter.gameObject.GetComponentInParent<ItemUI>();
                if(tmpItemUI)
                {
                    //空格子
                    if(tmpItemUI.indexOfDataInBox==-1)
                    {
                        SwapWithTarget(tmpItemUI.Index);
                        return;
                    }
                }
            }
            dragDefault();  
        }   
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
        //Data
        //设置目标格子在数据列表中的item数据
        InventoryManager.Instance.warehousePlayer.list[index]=GetItemData(InventoryManager.Instance.dragOrigItemUI.indexOfDataInBox);
        
        InventoryManager.Instance.warehousePlayer.list[index].imageboxIndex=index;

        InventoryManager.Instance.warehousePlayer.list[InventoryManager.Instance.dragOrigItemUI.indexOfDataInBox]=null;
        //UI
        InventoryManager.Instance.warehouseContainer.itemUIs[index].transform.GetChild(0).SetParent(InventoryManager.Instance.dragOrigParent,false);
        transform.SetParent(InventoryManager.Instance.warehouseContainer.itemUIs[index].transform,true);
        transform.position=InventoryManager.Instance.warehouseContainer.itemUIs[index].transform.position;

        InventoryManager.Instance.warehouseContainer.itemUIs[index].indexOfDataInBox=index;
        InventoryManager.Instance.dragOrigItemUI.indexOfDataInBox=-1;

        InventoryManager.Instance.warehouseContainer.itemUIs[index].RefreshImageAndText();
        InventoryManager.Instance.dragOrigItemUI.RefreshImageAndText();
    }
}
