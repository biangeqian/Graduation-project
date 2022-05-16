using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCharacterControllerMove : MonoBehaviour
{
    [Header("Components")]
    public GameObject deathCanvas;
    public GameObject openDropBoxUI;
    public float rayDistance=3f;
    private CharacterController characterController;
    private Animator characterAnimator;
    private Transform characterTransform;
    private GameObject inventory;
    private CharacterStats playerCharacterStats;
    private InventoryData_SO dropData;

    [Header("Property")]
    private Vector3 moveDirection;
    public float runSpeed;
    public float walkSpeed;
    public float crouchSpeed;
    public float powerRecoverSpeed=10f;
    public float runPowerCostSpeed=20f;
    public float jumpPowerCost=7f;
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

    private void Awake() 
    {
        GameManager.Instance.Player=gameObject;
    }
    private void Start() 
    {
        inventory=GameManager.Instance.CanvasInventory.gameObject;
        playerCharacterStats=GetComponent<CharacterStats>();
        if(GameManager.Instance.equipHelmet)
        {
            playerCharacterStats.CurrentDefence=2;
        }
        else
        {
            playerCharacterStats.CurrentDefence=0;
        }
        //隐藏鼠标
        Cursor.lockState = CursorLockMode.Locked;

        characterController=GetComponent<CharacterController>();
        characterAnimator=GetComponentInChildren<Animator>();
        characterTransform=transform;
        originHeight=characterController.height;
    }
    private void Update() 
    {
        if(playerCharacterStats.CurrentHealth<=0)
        {
            if(!deathCanvas.activeSelf)
            {
                GameManager.Instance.cleanStack();
                deathCanvas.SetActive(true);
                GameManager.Instance.CanvasStack.Push(deathCanvas);
                GameManager.Instance.CanvasInventory.GetComponent<InventoryManager>().cleanBag();
            }
            return;
        }
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
                    if(playerCharacterStats.CurrentPower>0)
                    {
                        moveDirection.y=JumpHeight;
                        playerCharacterStats.CurrentPower=Mathf.Max(playerCharacterStats.CurrentPower-jumpPowerCost,0f);
                    }   
                }
            }
            if(Input.GetKeyDown(KeyCode.C)&&GameManager.Instance.CanvasStack.Count==0)
            {
                var targetHeight=isCrouched?originHeight:CrouchHeight;
                if(!CrouchBlock) StartCoroutine(DoCrouch(targetHeight));
                isCrouched=!isCrouched; 
            }
            if(Input.GetKey(KeyCode.LeftShift)&&isCrouched&&GameManager.Instance.CanvasStack.Count==0)
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
            currentSpeed=(Input.GetKey(KeyCode.LeftShift)&&vertical>0f&&playerCharacterStats.CurrentPower>0)?runSpeed:walkSpeed;
        }  
        moveDirection.y-=Gravity*Time.deltaTime;
        characterController.Move(moveDirection*currentSpeed*Time.deltaTime);

        _Run=(currentSpeed==runSpeed)?true:false;

        SetAnimator();

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(inventory.activeSelf)
            {
                inventory.SetActive(false);
                GameManager.Instance.CanvasStack.Pop();
            }
            else
            {
                inventory.SetActive(true);
                GameManager.Instance.CanvasStack.Push(inventory);
                inventory.GetComponent<CanvasInventory>().setBattleModel();
            }
        }
        if(rayCheck()&&GameManager.Instance.CanvasStack.Count==0)
        {
            //显示F
            openDropBoxUI.SetActive(true);
            if(Input.GetKeyDown(KeyCode.F))
            {
                var canvas_inventory=GameManager.Instance.CanvasInventory;
                canvas_inventory.SetActive(true);
                canvas_inventory.GetComponent<CanvasInventory>().setDropBoxModel();
                canvas_inventory.GetComponent<InventoryManager>().dropBoxContainer.loadData(GameManager.Instance.curDropData);
                GameManager.Instance.CanvasStack.Push(canvas_inventory);
            }
        }
        else
        {
            //隐藏F
            openDropBoxUI.SetActive(false);
        }
        //奔跑消耗体力
        if(_Run)
        {
            playerCharacterStats.CurrentPower=Mathf.Max(playerCharacterStats.CurrentPower-runPowerCostSpeed*Time.deltaTime,0f);
        }
        //体力随时间恢复
        if(playerCharacterStats.CurrentPower<100)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {

            }
            else
            {
                playerCharacterStats.CurrentPower=Mathf.Min(playerCharacterStats.CurrentPower+powerRecoverSpeed*Time.deltaTime,100f);
            }   
            
        }
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
    private bool rayCheck()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 100));
        //Ray ray=new Ray(rayPoint.transform.position,rayPoint.transform.forward);
        RaycastHit hit;
        UnityEngine.Debug.DrawRay(ray.origin,ray.direction*rayDistance,Color.red);
        if(Physics.Raycast(ray,out hit,rayDistance))
        {
            //UnityEngine.Debug.Log(hit.collider.gameObject.tag);
            if(hit.collider.gameObject.CompareTag("DropBox"))
            {
                GameManager.Instance.curDropData=hit.collider.gameObject.GetComponentInParent<DropBox>().dropBoxCur;
                return true;
            }
        }
        return false;
    }
}
