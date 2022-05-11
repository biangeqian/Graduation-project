using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeHit : MonoBehaviour
{
    public bool isHitEnemy;
    public GameObject hitTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision) 
	{
        if (collision.gameObject.tag == "Enemy")
        {
            isHitEnemy=true;
            hitTarget=collision.gameObject;
        }
    }
    private void OnTriggerExit(Collider collision)
	{
        if (collision.gameObject.tag == "Enemy")
        {
            isHitEnemy=false;
            hitTarget=null;
        }
    }
}
