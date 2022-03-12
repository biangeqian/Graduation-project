using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootStepListener : MonoBehaviour
{
    public FootStepAudioData_SO footStepAudioData;
    public AudioSource audioSource;
    public LayerMask layerMask;
    
    private FPSCharacterControllerMove fPSCharacterControllerMove;
    private CharacterController characterController;
    private Vector3 footStepPosition;
    private float nextPlayTime;
    private void Start() 
    {
        characterController=GetComponent<CharacterController>();
        fPSCharacterControllerMove=GetComponent<FPSCharacterControllerMove>();
    }
    private void FixedUpdate() 
    {
        if(characterController.isGrounded)
        {
            if(characterController.velocity.normalized.magnitude>0.01f)
            {
                nextPlayTime+=Time.deltaTime;
                footStepPosition=transform.position+new Vector3(0,characterController.center.y,0)+Vector3.down*(characterController.height/2);
                bool isHit=Physics.Linecast(footStepPosition,footStepPosition+Vector3.down*+characterController.skinWidth,out RaycastHit hitInfo,layerMask);
                if(isHit)
                {
                    foreach(var audioElement in footStepAudioData.FootStepAduios)
                    
                    {
                        if(hitInfo.collider.CompareTag("FootAudio_"+audioElement.Tag))
                        {
                            if(fPSCharacterControllerMove.isCrouched)
                            {
                                if(nextPlayTime>=audioElement.Delay*2)
                                {
                                    // int audioCount=audioElement.AudioClips.Count;
                                    // int audioIndex=UnityEngine.Random.Range(0,audioCount);
                                    // AudioClip footstepAudioClip=audioElement.AudioClips[audioIndex];
                                    AudioClip footstepAudioClip=audioElement.AudioClips[0];
                                    audioSource.clip=footstepAudioClip;
                                    audioSource.Play();
                                    nextPlayTime=0;
                                }
                            }
                            else if(fPSCharacterControllerMove._Run)
                            {
                                if(nextPlayTime>=audioElement.Delay/2)
                                {
                                    // int audioCount=audioElement.AudioClips.Count;
                                    // int audioIndex=UnityEngine.Random.Range(0,audioCount);
                                    // AudioClip footstepAudioClip=audioElement.AudioClips[audioIndex];
                                    AudioClip footstepAudioClip=audioElement.AudioClips[0];
                                    audioSource.clip=footstepAudioClip;
                                    audioSource.Play();
                                    nextPlayTime=0;
                                }
                            }
                            else
                            {
                                if(nextPlayTime>=audioElement.Delay)
                                {
                                    // int audioCount=audioElement.AudioClips.Count;
                                    // int audioIndex=UnityEngine.Random.Range(0,audioCount);
                                    // AudioClip footstepAudioClip=audioElement.AudioClips[audioIndex];
                                    AudioClip footstepAudioClip=audioElement.AudioClips[0];
                                    audioSource.clip=footstepAudioClip;
                                    audioSource.Play();
                                    nextPlayTime=0;
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}
