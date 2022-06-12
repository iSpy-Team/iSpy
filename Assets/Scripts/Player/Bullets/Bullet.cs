using System;
using System.Collections;
using Mirror;
using UnityEngine;

namespace Player.Bullets
{
    public class Bullet : NetworkBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody2D;

        private float _damage = 0;

        private PlayerShoot owner = null;

        private void Start()
        {
            StartCoroutine(nameof(DestroyBullet));
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("AI") || col.CompareTag("Bullet")) return;

            if (col.gameObject.CompareTag("Player"))
            {
                Debug.Log("Fire "+col.gameObject.name);
                try
                {
                    PlayerManager target = col.GetComponent<PlayerManager>();
                    if (target == null) return;
                    target.GetComponent<PlayerManager>().DamageTo(target, _damage);
                    target.GetComponent<PlayerManager>().UpdateSprite(col.GetComponent<SpriteRenderer>(), Color.red);
                } catch(Exception e)
                {
                    Debug.Log(e.Message);
                }
            }

            Destroy(gameObject);
        }

        /// <summary>
        ///   <para>Moving Object Bullet</para>
        /// </summary>
        /// <param name="direction">Set direction bullet when instantiated </param>
        /// <param name="speed">Set speed bullet when instantiated</param>
        public void Move(Vector2 direction, float speed)
        {
            rigidbody2D.velocity = direction * speed;
        }

        /// <summary>
        ///   <para>Set Bullet Damage When Trigger With Another Player</para>
        /// </summary>
        /// <param name="damage">Set value damage</param>
        public void Damage(float damage)
        {
            _damage = damage;
        }

        /// <summary>
        ///   <para>Set owner client of this bullet.</para>
        /// </summary>
        /// <param name="player">Target player.</param>
        public void SetOwner(PlayerShoot player)
        {
            owner = player;
        }

        /// <summary>
        ///   <para>Destroy bullet after 5 seconds.</para>
        /// </summary>
        private IEnumerator DestroyBullet()
        {
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }
    }
}