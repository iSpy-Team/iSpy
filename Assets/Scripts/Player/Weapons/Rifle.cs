using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Weapons
{
    public class Rifle : Weapon
    {
        public override void SwapWeapon(int amount)
        {
            playerManager.WeaponType = WeaponType.Rifle;

            //playerManager.GetWeapon();
            //playerManager.SetWeapon(WeaponType.Pistol);
            playerManager.ItemPlayer.amount = amount;

            //PlayerManager.Instance.WeaponType = WeaponType.Pistol;
            //
            //PlayerManager.Instance.GetWeapon();
            //PlayerManager.Instance.SetWeapon(WeaponType.Pistol);
            //PlayerManager.Instance.ItemPlayer.Amount = amount;
        }
    }
}

