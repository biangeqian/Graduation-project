using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    [SerializeField]
    private Transform characterBodyTransform;
    private Vector3 cameraRotation;
    private Vector3 characterBodyRotation;
    public float xSpeed=5f;
    public float ySpeed=5f;
    public float yAngleMinLimit=-50f;
    public float yAngleMaxLimit=50f;

    public bool isShooting;
    private bool ifRecover;
    private bool ifMark;
    public float recoilUp;
    private float tmp_recoilUp;
    public float recoilHorizontal;
    public float recoverSpeed=2f;
    private Vector3 pre_angles;
    private float deltaUp;
    private float deltaHorizontal;


    // Start is called before the first frame update
    void Start()
    {
        if(!characterBodyTransform)
        {
            UnityEngine.Debug.Log("未绑定人物模型");
        }
        tmp_recoilUp=recoilUp;
    }
    void LateUpdate()
    {
        var MouseX=Input.GetAxis("Mouse X");
        var MouseY=Input.GetAxis("Mouse Y");
        cameraRotation.x-=MouseY*ySpeed;
        cameraRotation.x=ClampAngle(cameraRotation.x,yAngleMinLimit,yAngleMaxLimit);
        characterBodyRotation.y+=MouseX*xSpeed;

        if(MouseX!=0||MouseY!=0)
        {
            ifRecover=false;
        }
        if(isShooting)
        {
            if(MouseX!=0||MouseY!=0)
            {
                pre_angles=transform.eulerAngles;
            }
            else
            {
                ifRecover=true;
            }
            ifMark=false;
            cameraRotation.x-=tmp_recoilUp;
            tmp_recoilUp*=0.995f;
            characterBodyRotation.y+=Random.Range(-recoilHorizontal,recoilHorizontal);
        }
        else
        {
            tmp_recoilUp=recoilUp;
            if(ifRecover)
            {
                if(!ifMark)
                {
                    deltaUp=FixAngle(transform.eulerAngles.x)-FixAngle(pre_angles.x);
                    
                    deltaHorizontal=transform.eulerAngles.y-pre_angles.y;
                    // 0~360 去除分界点情况
                    if(deltaHorizontal>30f)
                    {
                        deltaHorizontal-=360f;
                    }
                    else if(deltaHorizontal<-30f)
                    {
                        deltaHorizontal+=360f;
                    }

                    ifMark=true;
                }
                
                if(Mathf.Abs(transform.eulerAngles.x-pre_angles.x)>(-deltaUp/10))
                {
                    cameraRotation.x-=deltaUp*Time.deltaTime*recoverSpeed;
                    characterBodyRotation.y-=deltaHorizontal*Time.deltaTime*recoverSpeed;  
                }         
            }
            else
            {
                pre_angles=transform.eulerAngles;
            }   
        }

        characterBodyTransform.rotation=Quaternion.Euler(0,characterBodyRotation.y,0);
        transform.rotation=Quaternion.Euler(cameraRotation.x,characterBodyRotation.y,0); 
    }
    static float ClampAngle(float angle,float min,float max)
    {
        if(angle<-360)
            angle+=360;
        if(angle>360)
            angle-=360;
        return Mathf.Clamp(angle,min,max);
    } 
    //欧拉角为负数时会+360存储    -60~0  360~300
    static float FixAngle(float angle)
    {
        if(angle>100f) return angle-360f;
        else return angle;
    }
}
