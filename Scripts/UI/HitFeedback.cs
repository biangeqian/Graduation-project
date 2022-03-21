using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFeedback : MonoBehaviour
{
    public float delayTime=0.2f;
    public float time=0f;
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.HitFeedbackUI=this.gameObject;
    }
    private void OnEnable() 
    {
        time=delayTime;
    }
    // Update is called once per frame
    void Update()
    {
        if(time<=0)
        {
            time=0;
            this.gameObject.SetActive(false);
        }
        else
        {
            time-=Time.deltaTime;
        }
    }
    public void addTime()
    {
        time+=delayTime;
    }
}
