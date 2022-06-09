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

        private GameObject targetPlayer;

        //public bool GetShoot() => _shoot;
        public WeaponSwap GetWeapon() => weapon;

        [SyncVar][SerializeField]
        private bool goFire = false;

        private Vector2 bulletDir;

        private void Start()
        {
            weapon = GetComponent<WeaponSwap>();
        }
        
        private void Update()
        {
            SetWeapon();         
        }

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
                    Fire(_selected.Speed, _selected.Damage, bulletDir);

                    if (hasAuthority)
                    {
                        weapon.DecreaseBullet(1);
                    }
                }

                _timer = 0f; 
            }
        }

        private void SetSfx()
        {
            //if (_selected.WeaponType == WeaponType.Pistol)
            //{
                //_audioSource.PlayOneShot(_clipSenjata1);                
            //}
            //else if (_selected.WeaponType == WeaponType.Shotgun)
            //{
                _audioSource.PlayOneShot(_clipSenjata1);    
            //}

        }

        private void Fire(float speed, float damage, Vector2 dir)
        {
            /*var bulletPool = BulletPool.Instance.GetBullet(); // get object bullet pool
            if (bulletPool == null) return; // return method if bullet pool equals null

            bulletPool.SetActive(true); // set active 
            bulletPool.transform.position = _position;*/

            var up = -transform.up;
            var bul = Instantiate(bullet, _position, Quaternion.identity); 
            var newBullet = bul.GetComponent<Bullet>(); // get script bullet
            newBullet.SetOwner(this);
            newBullet.Move(dir, speed); // call method move for moving bullet 
            newBullet.Damage(damage); // set damage value
        }

        [Command]
        public void GoFire(bool b)
        {
            goFire = b;
        }

        private void Shoot(Weapon gun, WeaponSwap equip)
        {
            if (targetPlayer)
            {
                var direction = targetPlayer.transform.position - transform.position;

                bulletDir =  direction;

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

        public void TargetPlayer(GameObject other)
        {
            if (!targetPlayer)
            {
                targetPlayer = other;
            }
        }
    }
}