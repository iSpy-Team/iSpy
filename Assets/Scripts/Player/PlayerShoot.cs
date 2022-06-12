using Mirror;
using Player.Bullets;
using Player.Weapons;
using UnityEngine;
using UnityEngine.UI;
using Color = System.Drawing.Color;

namespace Player
{
    public class PlayerShoot : NetworkBehaviour
    {
        [Header("Properties")] [SerializeField]
        private float distance = 10f;

        // Properties
        private float _timer;

        //Component
        private Vector2 _position;
        private Weapon _selected;

        [SerializeField]private AudioSource _audioSource;
        [SerializeField] private AudioClip _clipSenjata1, _clipSenjata2;

        private WeaponSwap weapon;

        [SerializeField] private GameObject bullet;

        [SerializeField] private GameObject targetPlayer;

        public WeaponSwap GetWeapon() => weapon;

        [SyncVar][SerializeField]
        private bool goFire = false;

        private void Start()
        {
            weapon = GetComponent<WeaponSwap>();
        }
        
        private void Update()
        {
            SetWeapon();         
        }

        /// <summary>
        ///   <para>Set weapon that player pick up.</para>
        /// </summary>
        private void SetWeapon()
        { 
            if (weapon.GetWeapon())
            {
                _selected = weapon.GetWeapon();
                _position = _selected.OriginShoot.position;

                _timer += Time.deltaTime;
                if ((!(_timer >= _selected.FireSpeed))) return;
                
                if (_selected.amount <= 0) return;
                
                //sfx weapons
                SetSfx();
                
                Shoot(_selected, weapon);

                if (goFire)
                {
                    Fire(_selected.Speed, _selected.Damage);

                    if (hasAuthority)
                    {
                        weapon.DecreaseBullet(1);
                    }
                }

                _timer = 0f; 
            }
        }

        /// <summary>
        ///   <para>Play sfx when shoot.</para>
        /// </summary>
        private void SetSfx()
        {
            _audioSource.PlayOneShot(_clipSenjata1);
        }

        /// <summary>
        ///   <para>Fire bullet to shoot player.</para>
        /// </summary>
        /// <param name="speed">Set value speed</param>
        /// <param name="damage">Set value damage</param>
        private void Fire(float speed, float damage)
        {
            var up = -transform.up;
            var bul = Instantiate(bullet, _position, Quaternion.identity); 
            var newBullet = bul.GetComponent<Bullet>();
            newBullet.SetOwner(this);
            newBullet.Move(up, speed);
            newBullet.Damage(damage);
        }

        /// <summary>
        ///   <para>Command to set bool for detetect if player have to shoot.</para>
        /// </summary>
        /// <param name="b">Set value goFire true or false</param>
        [Command]
        public void GoFire(bool b)
        {
            goFire = b;
        }

        /// <summary>
        ///   <para>Method for player shoot.</para>
        /// </summary>
        private void Shoot(Weapon gun, WeaponSwap equip)
        {
            if (targetPlayer)
            {
                if (hasAuthority)
                {
                    GoFire(true);
                }
            }
            else
            {
                if (hasAuthority)
                {
                    GoFire(false);
                }
            }
        }

        /// <summary>
        ///   <para>To set target player to shoot.</para>
        /// </summary>
        public void TargetPlayer(GameObject other)
        {
            targetPlayer = other;
        }
    }
}