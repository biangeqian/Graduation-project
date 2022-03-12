using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public int Index=-1;
    //当前格子存放的数据在list中的序号,同时也是存放图片的格子的序号
    //从0开始
    public int indexOfDataInBox=-1;
    public Image image;
    public Text upText;
    public Text bottomText;

   
    private void Awake() 
    {
        image=transform.GetChild(0).GetChild(0).GetComponent<Image>();
        upText=transform.GetChild(0).GetChild(1).GetComponent<Text>();
        bottomText=transform.GetChild(0).GetChild(2).GetComponent<Text>();
    }
    public void RefreshImageAndText()
    {
        image=transform.GetChild(0).GetChild(0).GetComponent<Image>();
        upText=transform.GetChild(0).GetChild(1).GetComponent<Text>();
        bottomText=transform.GetChild(0).GetChild(2).GetComponent<Text>();
    }
    
}
