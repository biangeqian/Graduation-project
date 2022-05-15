using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsText : MonoBehaviour
{
    private Text text1;
    public float speed=0.5f;
    // Start is called before the first frame update
    void Start()
    {
        text1=GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(text1.color.a>0)
        {
            text1.color=new Color(text1.color.r,text1.color.g,text1.color.b,text1.color.a-speed*Time.deltaTime);
        }
    }
    public void setText(string t)
    {
        text1.text=t;
        text1.color=new Color(text1.color.r,text1.color.g,text1.color.b,1f);
    }
}
