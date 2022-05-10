using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ItemForDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerClickHandler,IPointerEnterHandler
{
    private bool canDrag;
    private int size;
    private int indexD;
    private Transform dragItem;

    private int dragBeginModel;
    private List<Item> dragBeginList;
    private ContainerUI dragBeginContainer;
    private int dragEndModel;
    private List<Item> dragEndList;
    private ContainerUI dragEndContainer;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(GetComponentInParent<ContainerUI>().marketData)
        {
            return;//商店物品不可拖动
        }
        //拖拽的格子有物体
        if(GetComponentInParent<ItemUI>().indexOfDataInBox!=-1)
        {
            canDrag=true;
            dragBeginModel=InventoryManager.Instance.CheckInventoryUI(eventData.position);
            if(dragBeginModel==1)
            {
                dragBeginList=InventoryManager.Instance.warehousePlayer.list;
                dragBeginContainer=InventoryManager.Instance.warehouseContainer;
            }
            else if(dragBeginModel==2)
            {
                dragBeginList=InventoryManager.Instance.bagPlayer.list;
                dragBeginContainer=InventoryManager.Instance.bagContainer;
            }
            else if(dragBeginModel==3)
            {
                dragBeginList=InventoryManager.Instance.safeBagPlayer.list;
                dragBeginContainer=InventoryManager.Instance.safeBagContainer;
            }
            
            //记录原始数据
            indexD=GetComponentInParent<ItemUI>().indexOfDataInBox;
            size=dragBeginList[indexD].itemData.boxSize;
            dragItem=dragBeginContainer.itemUIs[indexD].transform.GetChild(0);

            if(size==1)
            {
                dragBeginContainer.itemUIs[indexD].indexOfDataInBox=-1;
            }
            else if(size==10)
            {
                dragBeginContainer.itemUIs[indexD].indexOfDataInBox=-1;
                dragBeginContainer.itemUIs[indexD+1].indexOfDataInBox=-1;
                dragBeginContainer.itemUIs[indexD+2].indexOfDataInBox=-1;
                dragBeginContainer.itemUIs[indexD+3].indexOfDataInBox=-1;
                dragBeginContainer.itemUIs[indexD+4].indexOfDataInBox=-1;
                dragBeginContainer.itemUIs[indexD+dragBeginContainer.rowNumber].indexOfDataInBox=-1;
                dragBeginContainer.itemUIs[indexD+dragBeginContainer.rowNumber+1].indexOfDataInBox=-1;
                dragBeginContainer.itemUIs[indexD+dragBeginContainer.rowNumber+2].indexOfDataInBox=-1;
                dragBeginContainer.itemUIs[indexD+dragBeginContainer.rowNumber+3].indexOfDataInBox=-1;
                dragBeginContainer.itemUIs[indexD+dragBeginContainer.rowNumber+4].indexOfDataInBox=-1;
            }
            else if(size==4)
            {
                dragBeginContainer.itemUIs[indexD].indexOfDataInBox=-1;
                dragBeginContainer.itemUIs[indexD+1].indexOfDataInBox=-1; 
                dragBeginContainer.itemUIs[indexD+dragBeginContainer.rowNumber].indexOfDataInBox=-1;
                dragBeginContainer.itemUIs[indexD+dragBeginContainer.rowNumber+1].indexOfDataInBox=-1;
            }
            else if(size==2)
            {
                dragBeginContainer.itemUIs[indexD].indexOfDataInBox=-1;
                dragBeginContainer.itemUIs[indexD+1].indexOfDataInBox=-1; 
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
            dragEndModel=InventoryManager.Instance.CheckInventoryUI(eventData.position);
            if(dragEndModel==1)
            {
                dragEndList=InventoryManager.Instance.warehousePlayer.list;
                dragEndContainer=InventoryManager.Instance.warehouseContainer;
            }
            else if(dragEndModel==2)
            {
                dragEndList=InventoryManager.Instance.bagPlayer.list;
                dragEndContainer=InventoryManager.Instance.bagContainer;
            }
            else if(dragEndModel==3)
            {
                dragEndList=InventoryManager.Instance.safeBagPlayer.list;
                dragEndContainer=InventoryManager.Instance.safeBagContainer;
            }
            else
            {
                dragDefault();
                return;
            }
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
                    if(targetItemUI.Index/dragEndContainer.rowNumber==(targetItemUI.Index+4)/dragEndContainer.rowNumber)
                    {
                        if(check(targetItemUI.Index)&&check(targetItemUI.Index+1)&&check(targetItemUI.Index+2)&&check(targetItemUI.Index+3)&&check(targetItemUI.Index+4)
                        &&check(targetItemUI.Index+10)&&check(targetItemUI.Index+11)&&check(targetItemUI.Index+12)&&check(targetItemUI.Index+13)&&check(targetItemUI.Index+14))
                        {
                            SwapWithTarget(targetItemUI.Index);
                            return;
                        }
                    }
                }
                else if(size==4)
                {
                    if(targetItemUI.Index/dragEndContainer.rowNumber==(targetItemUI.Index+1)/dragEndContainer.rowNumber)
                    {
                        if(check(targetItemUI.Index)&&check(targetItemUI.Index+1)&&check(targetItemUI.Index+10)&&check(targetItemUI.Index+11))
                        {
                            SwapWithTarget(targetItemUI.Index);
                            return;
                        }
                    }
                }
                else if(size==2)
                {
                    if(targetItemUI.Index/dragEndContainer.rowNumber==(targetItemUI.Index+1)/dragEndContainer.rowNumber)
                    {
                        if(check(targetItemUI.Index)&&check(targetItemUI.Index+1))
                        {
                            SwapWithTarget(targetItemUI.Index);
                            return;
                        }
                    }
                }
            }
            dragDefault();  
        }   
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        ShopDialogue shopDialogue=GameManager.Instance.CanvasStack.Peek().GetComponent<ShopDialogue>();
        if(shopDialogue)
        {
            if(GetComponentInParent<ContainerUI>().marketData)
            {
                //买
                var marketData=GetComponentInParent<ContainerUI>().marketData;
                int index=eventData.pointerEnter.gameObject.GetComponentInParent<ItemUI>().indexOfDataInBox;
                if(index>=0)
                {
                    shopDialogue.setCurrentShopItem(marketData.list[index],0,index);
                }
            }
            else
            {
                //卖
                int index=eventData.pointerEnter.gameObject.GetComponentInParent<ItemUI>().indexOfDataInBox;
                if(index>=0)
                {
                    shopDialogue.setCurrentShopItem(InventoryManager.Instance.warehousePlayer.list[index],1,index);
                }
                
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
    private bool check(int index)
    {
        if(index>=0&&index<dragEndContainer.totalNumber)
        {
            if(dragEndContainer.itemUIs[index].indexOfDataInBox==-1)
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
            dragBeginContainer.itemUIs[indexD].indexOfDataInBox=indexD;
        }
        else if(size==10)
        {
            dragBeginContainer.itemUIs[indexD].indexOfDataInBox=indexD;
            dragBeginContainer.itemUIs[indexD+1].indexOfDataInBox=indexD;
            dragBeginContainer.itemUIs[indexD+2].indexOfDataInBox=indexD;
            dragBeginContainer.itemUIs[indexD+3].indexOfDataInBox=indexD;
            dragBeginContainer.itemUIs[indexD+4].indexOfDataInBox=indexD;
            dragBeginContainer.itemUIs[indexD+10].indexOfDataInBox=indexD;
            dragBeginContainer.itemUIs[indexD+11].indexOfDataInBox=indexD;
            dragBeginContainer.itemUIs[indexD+12].indexOfDataInBox=indexD;
            dragBeginContainer.itemUIs[indexD+13].indexOfDataInBox=indexD;
            dragBeginContainer.itemUIs[indexD+14].indexOfDataInBox=indexD;
        }
        else if(size==4)
        {
            dragBeginContainer.itemUIs[indexD].indexOfDataInBox=indexD;
            dragBeginContainer.itemUIs[indexD+1].indexOfDataInBox=indexD;
            dragBeginContainer.itemUIs[indexD+10].indexOfDataInBox=indexD;
            dragBeginContainer.itemUIs[indexD+11].indexOfDataInBox=indexD;
        }
        else if(size==2)
        {
            dragBeginContainer.itemUIs[indexD].indexOfDataInBox=indexD;
            dragBeginContainer.itemUIs[indexD+1].indexOfDataInBox=indexD;
        }
    }
    private Item GetItemData(int index)
    {
        return dragBeginList[index];
    }
    private void SwapWithTarget(int index)
    {
        if(size==1)
        {
            //Data
            //交换目标格子在数据列表中的item数据
            dragEndList[index]=GetItemData(indexD);
            dragBeginList[indexD]=null;
            //UI交换位置
            dragEndContainer.itemUIs[index].transform.GetChild(0).SetParent(InventoryManager.Instance.dragOrigParent,false);
            dragItem.SetParent(dragEndContainer.itemUIs[index].transform,false);
            dragItem.position=dragEndContainer.itemUIs[index].transform.position;
            //记录数据存放位置
            dragEndContainer.itemUIs[index].indexOfDataInBox=index;
            //刷新子物体
            dragEndContainer.itemUIs[index].RefreshImageAndText();
            dragBeginContainer.itemUIs[indexD].RefreshImageAndText();
        }
        else if(size==10)
        {
            //Data
            //交换目标格子在数据列表中的item数据
            dragEndList[index]=GetItemData(indexD);
            dragBeginList[indexD]=null;
            //UI交换位置
            dragEndContainer.itemUIs[index].transform.GetChild(0).SetParent(InventoryManager.Instance.dragOrigParent,false);
            dragItem.SetParent(dragEndContainer.itemUIs[index].transform,false);
            dragItem.position=dragEndContainer.itemUIs[index].transform.position;
            //记录数据存放位置
            dragEndContainer.itemUIs[index].indexOfDataInBox=index;
            dragEndContainer.itemUIs[index+1].indexOfDataInBox=index;
            dragEndContainer.itemUIs[index+2].indexOfDataInBox=index;
            dragEndContainer.itemUIs[index+3].indexOfDataInBox=index;
            dragEndContainer.itemUIs[index+4].indexOfDataInBox=index;
            dragEndContainer.itemUIs[index+dragEndContainer.rowNumber].indexOfDataInBox=index;
            dragEndContainer.itemUIs[index+dragEndContainer.rowNumber+1].indexOfDataInBox=index;
            dragEndContainer.itemUIs[index+dragEndContainer.rowNumber+2].indexOfDataInBox=index;
            dragEndContainer.itemUIs[index+dragEndContainer.rowNumber+3].indexOfDataInBox=index;
            dragEndContainer.itemUIs[index+dragEndContainer.rowNumber+4].indexOfDataInBox=index;
            //刷新子物体
            dragEndContainer.itemUIs[index].RefreshImageAndText();
            dragBeginContainer.itemUIs[indexD].RefreshImageAndText();
        }
        else if(size==4)
        {
            //Data
            //交换目标格子在数据列表中的item数据
            dragEndList[index]=GetItemData(indexD);
            dragBeginList[indexD]=null;
            //UI交换位置
            dragEndContainer.itemUIs[index].transform.GetChild(0).SetParent(InventoryManager.Instance.dragOrigParent,false);
            dragItem.SetParent(dragEndContainer.itemUIs[index].transform,false);
            dragItem.position=dragEndContainer.itemUIs[index].transform.position;
            //记录数据存放位置
            dragEndContainer.itemUIs[index].indexOfDataInBox=index;
            dragEndContainer.itemUIs[index+1].indexOfDataInBox=index; 
            dragEndContainer.itemUIs[index+dragEndContainer.rowNumber].indexOfDataInBox=index;
            dragEndContainer.itemUIs[index+dragEndContainer.rowNumber+1].indexOfDataInBox=index;
            //刷新子物体
            dragEndContainer.itemUIs[index].RefreshImageAndText();
            dragBeginContainer.itemUIs[indexD].RefreshImageAndText();
        }
        else if(size==2)
        {
            //Data
            //交换目标格子在数据列表中的item数据
            dragEndList[index]=GetItemData(indexD);
            dragBeginList[indexD]=null;
            //UI交换位置
            dragEndContainer.itemUIs[index].transform.GetChild(0).SetParent(InventoryManager.Instance.dragOrigParent,false);
            dragItem.SetParent(dragEndContainer.itemUIs[index].transform,false);
            dragItem.position=dragEndContainer.itemUIs[index].transform.position;
            //记录数据存放位置
            dragEndContainer.itemUIs[index].indexOfDataInBox=index;
            dragEndContainer.itemUIs[index+1].indexOfDataInBox=index; 
            //刷新子物体
            dragEndContainer.itemUIs[index].RefreshImageAndText();
            dragBeginContainer.itemUIs[indexD].RefreshImageAndText();
        }
    }
}
