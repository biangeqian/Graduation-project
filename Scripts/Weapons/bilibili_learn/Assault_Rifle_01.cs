using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scripts.weapon
{
    public class Assault_Rifle_01 :FireArms
    {
        protected override void Shooting()
        {
            UnityEngine.Debug.Log("shoot");
        }
        protected override void Reload()
        {
            CurrentAmmoInMag=AmmoInMag;
            CurrentMaxAmmoCarried-=AmmoInMag;
        }
        private void Update() 
        {
            if(Input.GetMouseButton(0))
            {
                DoAttack();
            }
            if(Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
        }
    }
}

