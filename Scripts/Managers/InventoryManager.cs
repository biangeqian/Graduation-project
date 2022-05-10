using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(this);

        warehousePlayer=Instantiate(warehouseOrig);
        bagPlayer=Instantiate(bagOrig);
        safeBagPlayer=Instantiate(safeBagOrig);
    }
    [Header("Inventory Data")]
    public InventoryData_SO warehouseOrig;
    public InventoryData_SO warehousePlayer;
    public InventoryData_SO bagOrig;
    public InventoryData_SO bagPlayer;
    public InventoryData_SO safeBagOrig;
    public InventoryData_SO safeBagPlayer;

    [Header("Container UI")]
    public ContainerUI warehouseContainer;
    public ContainerUI bagContainer;
    public ContainerUI safeBagContainer;

    [Header("Drag")]
    public GameObject dragArea;
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
    public int CheckInventoryUI(Vector3 position)
    {
        RectTransform t1=warehouseContainer.transform as RectTransform;
        RectTransform t2=bagContainer.transform as RectTransform;
        RectTransform t3=safeBagContainer.transform as RectTransform;
        if(RectTransformUtility.RectangleContainsScreenPoint(t1,position))
        {
            UnityEngine.Debug.Log("仓库区域");
            return 1;
        }
        else if(RectTransformUtility.RectangleContainsScreenPoint(t2,position))
        {
            UnityEngine.Debug.Log("背包区域");
            return 2;
        }
        else if(RectTransformUtility.RectangleContainsScreenPoint(t3,position))
        {
            UnityEngine.Debug.Log("安全箱区域");
            return 3;
        }
        return 0;
    }
    #endregion
}
