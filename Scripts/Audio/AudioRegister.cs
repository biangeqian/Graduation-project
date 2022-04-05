using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRegister : MonoBehaviour
{
    void Awake()
    {
        GameManager.Instance.SettingUI.GetComponent<Canvas_settings>().audioSource=GetComponent<AudioSource>();
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
