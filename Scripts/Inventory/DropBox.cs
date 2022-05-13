using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour
{
    public InventoryData_SO dropBoxOrig;
    public InventoryData_SO dropBoxCur;
    private void Awake() 
    {
        dropBoxCur=Instantiate(dropBoxOrig);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
