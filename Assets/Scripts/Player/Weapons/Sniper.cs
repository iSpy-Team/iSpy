using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Weapons
{
    public class Sniper : Weapon
    {
        public override void SwapWeapon(int amount)
        {
            playerManager.WeaponType = WeaponType.Sniper;
            //playerManager.GetWeapon();
            //playerManager.SetWeapon(WeaponType.Shotgun);
            playerManager.ItemPlayer.amount = amount;

            //PlayerManager.Instance.WeaponType = WeaponType.Shotgun;
            //PlayerManager.Instance.GetWeapon();
            //PlayerManager.Instance.SetWeapon(WeaponType.Shotgun);
            //PlayerManager.Instance.ItemPlayer.Amount = amount;
        }
    }
}

