using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        warehousePlayer=Instantiate(warehouseOrig);
    }
    [Header("Inventory Data")]
    public InventoryData_SO warehouseOrig;
    public InventoryData_SO warehousePlayer;
    public InventoryData_SO actionOrig;
    public InventoryData_SO actionPlayer;
    public InventoryData_SO equipmentOrig;
    public InventoryData_SO equipmentPlayer;

    [Header("Container UI")]
    public ContainerUI warehouseContainer;
    public ContainerUI actionContainer;
    public ContainerUI equipmentContainer;

    [Header("Drag")]
    public GameObject dragArea;
    public ItemUI dragOrigItemUI;
    public RectTransform dragOrigParent;
    // Start is called before the first frame update
    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region 检查拖拽物品是否在UI范围内
    public bool CheckInWarehouseUI(Vector3 position)
    {
        RectTransform t=warehouseContainer.transform as RectTransform;
        if(RectTransformUtility.RectangleContainsScreenPoint(t,position))
        {
            return true;
        }
        return false;
    }
    #endregion
}
