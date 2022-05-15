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
        helmetPlayer=Instantiate(helmetOrig);
        bigGun1Player=Instantiate(bigGun1Orig);
    }
    [Header("Inventory Data")]
    public InventoryData_SO warehouseOrig;
    public InventoryData_SO warehousePlayer;
    public InventoryData_SO bagOrig;
    public InventoryData_SO bagPlayer;
    public InventoryData_SO safeBagOrig;
    public InventoryData_SO safeBagPlayer;
    public InventoryData_SO helmetOrig;
    public InventoryData_SO helmetPlayer;
    public InventoryData_SO bigGun1Orig;
    public InventoryData_SO bigGun1Player;

    [Header("Container UI")]
    public ContainerUI warehouseContainer;
    public ContainerUI bagContainer;
    public ContainerUI safeBagContainer;
    public ContainerUI dropBoxContainer;
    public ContainerUI helmetContainer;
    public ContainerUI bigGun1Container;

    [Header("Drag")]
    public GameObject dragArea;
    public RectTransform dragOrigParent;
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        gameObject.SetActive(false);
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
        RectTransform t4=dropBoxContainer.transform as RectTransform;
        RectTransform t5=helmetContainer.transform as RectTransform;
        RectTransform t6=bigGun1Container.transform as RectTransform;
        if(RectTransformUtility.RectangleContainsScreenPoint(t1,position)&&warehouseContainer.gameObject.activeSelf)
        {
            //UnityEngine.Debug.Log("仓库区域");
            return 1;
        }
        else if(RectTransformUtility.RectangleContainsScreenPoint(t2,position))
        {
            //UnityEngine.Debug.Log("背包区域");
            return 2;
        }
        else if(RectTransformUtility.RectangleContainsScreenPoint(t3,position))
        {
            //UnityEngine.Debug.Log("安全箱区域");
            return 3;
        }
        else if(RectTransformUtility.RectangleContainsScreenPoint(t4,position)&&dropBoxContainer.gameObject.activeSelf)
        {
            //UnityEngine.Debug.Log("战利品箱区域");
            return 4;
        }
        else if(RectTransformUtility.RectangleContainsScreenPoint(t5,position))
        {
            //UnityEngine.Debug.Log("头盔区域");
            return 5;
        }
        else if(RectTransformUtility.RectangleContainsScreenPoint(t6,position))
        {
            //UnityEngine.Debug.Log("主武器1区域");
            return 6;
        }
        return 0;
    }
    #endregion
    public void SaveData()
    {
        PlayerPrefs.SetInt("money",GameManager.Instance.playerMoney);
        SaveManager.Instance.Save(warehousePlayer,warehousePlayer.name);
        SaveManager.Instance.Save(bagPlayer,bagPlayer.name);
        SaveManager.Instance.Save(safeBagPlayer,safeBagPlayer.name);
        SaveManager.Instance.Save(helmetPlayer,helmetPlayer.name);
        SaveManager.Instance.Save(bigGun1Player,bigGun1Player.name);
    }
    public void LoadData()
    {
        GameManager.Instance.playerMoney=PlayerPrefs.GetInt("money");
        GetComponent<CanvasInventory>().updateMoney();
        SaveManager.Instance.Load(warehousePlayer,warehousePlayer.name);
        SaveManager.Instance.Load(bagPlayer,bagPlayer.name);
        SaveManager.Instance.Load(safeBagPlayer,safeBagPlayer.name);
        SaveManager.Instance.Load(helmetPlayer,helmetPlayer.name);
        SaveManager.Instance.Load(bigGun1Player,bigGun1Player.name);
        warehouseContainer.loadData(warehousePlayer);
        bagContainer.loadData(bagPlayer);
        safeBagContainer.loadData(safeBagPlayer);
        helmetContainer.loadData(helmetPlayer);
        bigGun1Container.loadData(bigGun1Player);
    }
    public void Remake()
    {
        GameManager.Instance.playerMoney=500000;
        warehousePlayer=Instantiate(warehouseOrig);
        bagPlayer=Instantiate(bagOrig);
        safeBagPlayer=Instantiate(safeBagOrig);
        helmetPlayer=Instantiate(helmetOrig);
        bigGun1Player=Instantiate(bigGun1Orig);
        SaveData();
        LoadData();
    }
    public void cleanBag()
    {
        bagPlayer=Instantiate(bagOrig);
        bagContainer.loadData(bagPlayer);
        helmetPlayer=Instantiate(helmetOrig);
        helmetContainer.loadData(helmetPlayer);
        bigGun1Player=Instantiate(bigGun1Orig);
        bigGun1Container.loadData(bigGun1Player);
    }
}
