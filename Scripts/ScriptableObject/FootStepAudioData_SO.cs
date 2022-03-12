using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FPS/Foot Step Aduio Data")]
public class FootStepAudioData_SO : ScriptableObject
{
    public List<FootStepAudio> FootStepAduios=new List<FootStepAudio>();
}
[System.Serializable]
public class FootStepAudio
{
    public string Tag;
    public List<AudioClip> AudioClips=new List<AudioClip>();
    public float Delay=0.6f;//间隔
}