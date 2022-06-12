using UnityEngine;
using Mirror;

namespace Player.Weapons
{
    public class Weapon : NetworkBehaviour
    {
        [Header("Components")] [SerializeField]
        private WeaponType weaponType;
        [SerializeField] protected Transform originShoot;
        [SerializeField] protected Sprite mountSprite, dropSprite;

        [Header("Properties")]
        [SerializeField] protected float damage;
        [SerializeField] protected float speed;

        [SyncVar]
        public int amount;
        [SerializeField] protected float fireSpeed;

        protected PlayerManager playerManager;

        private void Start()
        {
            playerManager = GetComponentInParent<PlayerManager>();
            dropSprite = GetComponent<SpriteRenderer>().sprite;
        }

        public virtual float Damage => damage;
        public virtual float Speed => speed;

        public virtual float FireSpeed => fireSpeed;

        public virtual Transform OriginShoot => originShoot;

        public virtual int Amount
        {
            get => amount;
            set => amount = value;
        }

        public virtual void SwapWeapon(int amount)
        {
            playerManager.WeaponType = weaponType;
        }

        public void ChgToDropSprite()
        {
            GetComponent<SpriteRenderer>().sprite = dropSprite;
        }

        public void ChgToMountSprite()
        {
            GetComponent<SpriteRenderer>().sprite = mountSprite;
        }

        [Command(requiresAuthority =false)]
        private void CmdDecreaseBullet(int number)
        {
            amount -= number;
        }
        
        public void DecreaseBullet(int number)
        {
            CmdDecreaseBullet(number);
        }

        public WeaponType WeaponType => weaponType;
    }
}