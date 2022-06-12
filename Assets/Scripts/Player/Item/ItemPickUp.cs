using System;
using Player.Weapons;
using UnityEngine;

namespace Player.Item
{
    public class ItemPickUp : MonoBehaviour
    {
        public ItemChoice itemChoice;

        public WeaponType bulletType;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("Player")) return;

            var player = col.gameObject;
        
            switch (itemChoice)
            {
                case ItemChoice.Amount:
                    PickUpItemAmount(player.GetComponent<WeaponSwap>());
                    break;
                case ItemChoice.Health:
                    PickUpItemHealth(player.GetComponent<PlayerManager>());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        ///   <para>To call method heal if player pickup item heal.</para>
        /// </summary>
        /// <param name="player">Target player.</param>
        private void PickUpItemHealth(PlayerManager player)
        {
            player.Heal(10);
            Destroy(gameObject);
        }

        /// <summary>
        ///   <para>To call method SetWeapon if player pickup weapon.</para>
        /// </summary>
        /// <param name="player">Target player.</param>
        private void PickUpItemAmount(WeaponSwap player)
        {
            player.SetWeapon(gameObject.GetComponent<Weapon>());
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}