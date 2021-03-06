using Mirror;
using UnityEngine;

public class Soldier : NetworkBehaviour
{
    [Header("Target")] [SerializeField] protected GameObject targetPlayer, child;

    [SerializeField] protected GameObject bullet;

    [Header("Properties")] [SerializeField]
    protected float delay;

    protected float timerToDelay;

    [Header("Soldier Properties")]
    [SerializeField] private SoldierType type;
    [SerializeField] private float moveSpeed;

    private float maxDistance;
    public float distance;

    [SerializeField] protected float fireSpeed;
    protected float timerToFire;

    [Header("Shoot Properties")] [SerializeField]
    protected float bulletSpeed;

    [SerializeField] protected float damage;

    [SerializeField] protected Transform _originShoot;

    [SerializeField] private DetectionPlayer _detectionPlayer;

    private Vector3 direction;

    [SyncVar]
    public bool moveForward;

    public void Start()
    {
        direction = transform.up;

        maxDistance = transform.position.magnitude + distance;

        Flip(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
        timerToDelay = delay;
    }

    private void Update()
    {
        if (_detectionPlayer.detection)
        {
            Attack();
        }

        NpcMove();
    }

    private void NpcMove()
    {
        if (isServer)
        {
            if (_detectionPlayer.detection)
            {
                Move(0);
            }
            else
            {
                Move(moveSpeed);
            }
        }
    }

    /// <summary>
    ///   <para>Moving soldier NPC. Only run on the server.</para>
    /// </summary>
    /// <param name="speed">Set value speed.</param>
    [Server]
    private void Move(float speed)
    {
        if (moveForward)
        {
            transform.position += direction * speed * Time.deltaTime;
            
            if (transform.position.magnitude > maxDistance)
            {
                transform.position += -direction * speed * Time.deltaTime;
                moveForward = false;
                Flip(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            transform.position += -direction * speed * Time.deltaTime;
            
            if (transform.position.magnitude > maxDistance)
            {
                transform.position += direction * speed * Time.deltaTime;
                moveForward = true;
                Flip(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            }
        }
    }

    /// <summary>
    ///   <para>To flip soldier sprite.</para>
    /// </summary>
    /// <param name="x">Set value x.</param>
    /// <param name="y">Set value y.</param>
    /// <param name="z">Set value z.</param>
    private void Flip(float x, float y, float z)
    {
        transform.localScale = new Vector3(x, y, z);
        child.transform.localEulerAngles = new Vector3(0, 0, child.transform.localEulerAngles.z + 180);
    }

    /// <summary>
    ///   <para>Method for soldier attack.</para>
    /// </summary>
    private void Attack()
    {
        timerToFire += Time.deltaTime;
        if (timerToFire < fireSpeed) return;

        var objBullet = Instantiate(bullet, _originShoot.position, _originShoot.rotation);
        objBullet.GetComponent<Rigidbody2D>().velocity =
            (transform.localScale.y < 0) ? _originShoot.up * -bulletSpeed : _originShoot.up * bulletSpeed;
        objBullet.GetComponent<BulletNPC>().Damage(damage);

        Destroy(objBullet, 5);

        timerToFire = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") return;

        moveForward = !moveForward;
        Flip(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
    }
}

public enum SoldierType
{
    horizontal,
    vertical
}