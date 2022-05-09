using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDialogue : MonoBehaviour
{
    public GameObject Menu;
    private GameObject canvas_inventory;
    private void OnEnable() 
    {
        Menu.SetActive(false);
        canvas_inventory.SetActive(true);
        canvas_inventory.GetComponent<CanvasInventory>().setMarketModel();
    }
    private void OnDisable() 
    {
        Menu.SetActive(true);
        canvas_inventory.SetActive(false);
    }
    // Start is called before the first frame update
    void Awake()
    {
        canvas_inventory=GameManager.Instance.CanvasInventory;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
