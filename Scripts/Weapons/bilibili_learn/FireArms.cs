using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scripts.weapon
{
    public abstract class FireArms : MonoBehaviour,IWeapon
    {
        public Transform MuzzlePoint;//枪口位置
        public Transform CasingPoint;//弹壳抛出位置
        public ParticleSystem MuzzleParticle;
        public ParticleSystem CasingParticle;
        public float FireRate;//每秒射几发

        public int AmmoInMag=30;
        public int MaxAmmoCarried=120;

        protected int CurrentAmmoInMag;
        protected int CurrentMaxAmmoCarried;
        private float lastFireTime;
        protected Animator GunAnimator;
        protected bool isReloading;
        protected bool isInspecting;

        protected virtual void Start() 
        {
           CurrentAmmoInMag=AmmoInMag;
           CurrentMaxAmmoCarried=MaxAmmoCarried;
           GunAnimator=GetComponent<Animator>();
        }
        
        public void DoAttack()
        {
            if(CurrentAmmoInMag>0&&IsAllowShooting())
            {
                CurrentAmmoInMag-=1;
                Shooting();
                lastFireTime=Time.time;
            }
        }
        protected abstract void Shooting();
        protected abstract void Reload();

        private bool IsAllowShooting()
        {
            return Time.time-lastFireTime>1/FireRate;
        }

        protected void AnimationCheck () 
        {
            if (GunAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Reload Out Of Ammo") || 
                GunAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Reload Ammo Left")) 
            {
                isReloading = true;
            } 
            else 
            {
                isReloading = false;
            }
            if (GunAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Inspect")) 
            {
                isInspecting = true;
            } 
            else 
            {
                isInspecting = false;
            }
        }
    }

}
