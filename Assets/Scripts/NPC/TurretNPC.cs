using Mirror;
using UnityEngine;

public class TurretNPC : NetworkBehaviour
{
    [SerializeField] private GameObject bullet;

    private float _timerToFire;

    [Header("Shoot Properties")] [SerializeField]
    protected float bulletSpeed;

    [SerializeField] protected float damage;
    [SerializeField] private float fireSpeed;

    [Header("Turret Properties")] public float health = 100;

    [Header("Turret Components")] [SerializeField]
    private Transform originShoot;

    [SerializeField] private Transform turretWeapon;

    private NetworkIdentity _identity;

    [SerializeField]private DetectionPlayer _detectionPlayer;
    [SerializeField] private Transform weapon;

    private void Start()
    {
        _identity = GetComponent<NetworkIdentity>();
    }

    private void Update()
    {
        if (_detectionPlayer.detection)
        {   
            CmdAttack();
        }
    }

    /// <summary>
    ///   <para>Command to NPC attack.</para>
    /// </summary>
    private void CmdAttack()
    {
        _timerToFire += Time.deltaTime;
        if (_timerToFire < fireSpeed) return;

        var objBullet = Instantiate(bullet, originShoot.position, Quaternion.identity);

        objBullet.GetComponent<Rigidbody2D>().velocity = turretWeapon.up * bulletSpeed;
        objBullet.GetComponent<BulletNPC>().Damage(damage);

        Destroy(objBullet, 5f);
        _timerToFire = 0;
    }
}