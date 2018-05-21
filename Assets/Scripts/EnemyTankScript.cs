//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankScript : MonoBehaviour {

    private Rigidbody2D body;
    public float speed;
    public float turnSpeed;
    private SpriteRenderer spriteRenderer;
    private Pathfinding pathfinding;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    [SerializeField] public MapGenerator map_generator;
    [SerializeField] int gridSizeX, gridSizeY;
    [SerializeField] private GameObject TotalTank;
    [SerializeField] private GameObject ExplosionMark;


    [Header("Canon Settings")]
    public float canonBulletSpeed;
    public GameObject canonBulletPrefab;
    public Transform canonBulletSpawn;
    private float lastTimeShoot;
    public float timeToShoot;

    [Header("Health Settings")]
    public float healthPoints;
    private float maxHealthPoints = 100;

    public enum EnemyState
    {
        PATROL,
        FOLLOW,
        ATTACK,
        GO_BACK
    }
    private EnemyState enemyState = EnemyState.PATROL;

    private float distance;
    private Transform playerTransform;
    private EnemyTourelleScript enemyTourelleScript;

    // Use this for initialization
    void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        playerTransform = FindObjectOfType<PlayerScript>().transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyTourelleScript = FindObjectOfType<EnemyTourelleScript>();
        healthPoints = maxHealthPoints;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        AI();
        distance = Vector2.Distance(transform.position, playerTransform.transform.position);
        if (healthPoints < maxHealthPoints)
        {
            enemyState = EnemyState.FOLLOW;
        }
        if (distance <=5)
        {
            enemyState = EnemyState.ATTACK;
        }
        var hit = Physics2D.Raycast(transform.position, transform.forward, 5f, 0);
        //Debug.DrawRay(transform.position, transform.forward, Color.green, 0.1f);
        TotalDamage();
        Move();
    }

    private void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        Vector3 dir = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void Turn()
    {
        transform.Rotate(-Vector3.forward * turnSpeed * Time.deltaTime);
    }

        private void CanonShoot()
    {
        if (Time.realtimeSinceStartup - lastTimeShoot > timeToShoot)
        {
            GameObject Bullet = Instantiate(canonBulletPrefab, canonBulletSpawn.position, canonBulletSpawn.rotation);
            Bullet.GetComponent<Rigidbody2D>().velocity = canonBulletSpawn.up * canonBulletSpeed;
            Destroy(Bullet, 5);
            lastTimeShoot = Time.realtimeSinceStartup;
        }
    }

    private void AI()
    {
        switch (enemyState)
        {
            case EnemyState.PATROL:

                break;
            case EnemyState.FOLLOW:
                Move();
                break;
            case EnemyState.ATTACK:
                CanonShoot();
                break;
            case EnemyState.GO_BACK:

                break;
        }
    }

    private void TotalDamage()
    {
        if (healthPoints <= 0)
        {
            Destroy(TotalTank);
            healthPoints = 0;
            Instantiate(ExplosionMark, transform.position, Quaternion.identity);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            healthPoints = healthPoints - 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerCanonBullet")
        {
            healthPoints = healthPoints - 50;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "PlayerMachineGunBullet")
        {
            healthPoints = healthPoints - 2;
            Destroy(collision.gameObject);
        }
    }
}
