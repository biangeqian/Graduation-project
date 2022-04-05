using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCharacterControllerMove : MonoBehaviour
{
    [Header("Components")]
    private CharacterController characterController;
    private Animator characterAnimator;
    private Transform characterTransform;

    [Header("Property")]
    private Vector3 moveDirection;
    public float runSpeed;
    public float walkSpeed;
    public float crouchSpeed;
    public float Gravity=9.8f;
    public float JumpHeight;
    public float CrouchHeight=1f;//下蹲高度
    private float originHeight;//原始高度
    private float currentSpeed;
    private float horizontal=0;
    private float vertical=0;

    [Header("State")]
    public bool isCrouched;
    private bool CrouchBlock;//下蹲协程阻塞
    

    [Header("Aniamtor")]
    public bool _Run;
    public bool _Walk;

    private void Start() 
    {
        GameManager.Instance.Player=gameObject;

        //隐藏鼠标
        Cursor.lockState = CursorLockMode.Locked;

        characterController=GetComponent<CharacterController>();
        characterAnimator=GetComponentInChildren<Animator>();
        characterTransform=transform;
        originHeight=characterController.height;
    }
    private void Update() 
    {
        //isGrounded只记录最后一次调用Move后的位置
        if(characterController.isGrounded)
        {
            if(GameManager.Instance.CanvasStack.Count>0)
            {
                horizontal=0;
                vertical=0;
            }
            else
            {
                horizontal=Input.GetAxis("Horizontal");
                vertical=Input.GetAxis("Vertical");
            }
            if(vertical!=0)
            {
                horizontal*=0.4f;
            }
            else
            {
                horizontal*=0.8f;
            }
            moveDirection=new Vector3(horizontal,0,vertical);
            _Walk=(moveDirection==Vector3.zero)?false:true;
            //转全局坐标
            moveDirection=characterTransform.TransformDirection(moveDirection);

            if(Input.GetKeyDown(KeyCode.Space)&&GameManager.Instance.CanvasStack.Count==0)
            {
                if(isCrouched)
                {
                    if(!CrouchBlock) StartCoroutine(DoCrouch(originHeight));
                    isCrouched=!isCrouched;
                }
                else
                {
                    moveDirection.y=JumpHeight;
                }
            }
            if(Input.GetKeyDown(KeyCode.C)&&GameManager.Instance.CanvasStack.Count==0)
            {
                var targetHeight=isCrouched?originHeight:CrouchHeight;
                if(!CrouchBlock) StartCoroutine(DoCrouch(targetHeight));
                isCrouched=!isCrouched; 
            }
            if(Input.GetKeyDown(KeyCode.LeftShift)&&isCrouched&&GameManager.Instance.CanvasStack.Count==0)
            {
                if(!CrouchBlock) StartCoroutine(DoCrouch(originHeight));
                isCrouched=!isCrouched;
            }
            
        }
        if(isCrouched)
        {
            currentSpeed=crouchSpeed;
        }
        else
        {
            currentSpeed=(Input.GetKey(KeyCode.LeftShift)&&vertical>0)?runSpeed:walkSpeed;
        }  
        moveDirection.y-=Gravity*Time.deltaTime;
        characterController.Move(moveDirection*currentSpeed*Time.deltaTime);

        _Run=(currentSpeed==runSpeed)?true:false;

        SetAnimator();
    }
    private IEnumerator DoCrouch(float target)
    {
        CrouchBlock=true;
        float tmp_Velocity=0;
        while(Mathf.Abs(characterController.height-target)>0.01f)
        {
            yield return null;
            characterController.height=Mathf.SmoothDamp(characterController.height,target,ref tmp_Velocity,Time.deltaTime*20);
        }
        CrouchBlock=false;
    }
    private void SetAnimator()
    {
        characterAnimator.SetBool("Run",_Run);
        characterAnimator.SetBool("Walk",_Walk);
    }
}
