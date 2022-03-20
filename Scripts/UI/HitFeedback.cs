using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFeedback : MonoBehaviour
{
    public float delayTime=0.2f;
    private float time=0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(time<=0)
        {
            time=0;
            this.enabled=false;
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
