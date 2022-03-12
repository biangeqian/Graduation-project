using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMove : MonoBehaviour
{
    private Transform characterTransform;
    private Rigidbody characterRigidbody;
    public float jumpHeight;
    public float Speed;
    public float gravity;
    private bool isGrounded;
    private Vector3 currentVelocity;
    private void Start() 
    {
        characterTransform=transform;
        characterRigidbody=GetComponent<Rigidbody>();
    }
    private void Update() 
    {
        if(isGrounded)
        {
            if(Input.GetButtonDown("Jump"))
            {
                characterRigidbody.velocity=new Vector3(currentVelocity.x,CaculateJumpHeightSpeed(),currentVelocity.z);
            }
        }
    }
    private void FixedUpdate() 
    {
        if(isGrounded)
        {
            var horizontal=Input.GetAxis("Horizontal");
            var vertical=Input.GetAxis("Vertical");

            var currentDirection=new Vector3(horizontal,0,vertical);
            //转全局坐标
            currentDirection=characterTransform.TransformDirection(currentDirection);
            currentDirection*=Speed;

            currentVelocity=characterRigidbody.velocity;
            var velocityChange=currentDirection-currentVelocity;
            velocityChange.y=0;

            characterRigidbody.AddForce(velocityChange,ForceMode.VelocityChange);
        }
        characterRigidbody.AddForce(new Vector3(0,-gravity*characterRigidbody.mass,0));
    }
    private void OnCollisionStay(Collision other) 
    {
        isGrounded=true;
    }
    private void OnCollisionExit(Collision other) 
    {
        isGrounded=false;
    }
    private float CaculateJumpHeightSpeed()
    {
        return Mathf.Sqrt(2*gravity*jumpHeight);
    }
}
