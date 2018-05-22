//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankScript : MonoBehaviour {

    private Rigidbody2D body;
    public float speed;
    public float turnSpeed;
    private SpriteRenderer spriteRenderer;
    private Transform target;
    private Transform spawnPosition;
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

    private bool getDamage = false;

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
    private GameManager gameManager;

    // Use this for initialization
    void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        playerTransform = FindObjectOfType<PlayerScript>().transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyTourelleScript = FindObjectOfType<EnemyTourelleScript>();
        gameManager = FindObjectOfType<GameManager>();
        healthPoints = maxHealthPoints;
        spawnPosition = transform;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        AI();
        distance = Vector2.Distance(transform.position, playerTransform.transform.position);
        if (getDamage || distance <= 10)
        {
            enemyState = EnemyState.FOLLOW;

        }
        if (distance <= 5)
        {
            enemyState = EnemyState.ATTACK;
        }
        if (distance >= 15)
        {
            enemyState = EnemyState.GO_BACK;
        }
        TotalDamage();
    }

    private void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        Vector3 dir = target.position - transform.position;
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
                target = playerTransform;
                Move();
                break;
            case EnemyState.ATTACK:
                CanonShoot();
                break;
            case EnemyState.GO_BACK:
                target = spawnPosition;
                Move();
                break;
        }
    }

    private IEnumerator HeavyDamage()
    {

        yield return new WaitForSeconds(.1f);
        for (int i = 0; i < 3; i++)
        {
            GetComponent<SpriteRenderer>().color = Color.clear;
            yield return new WaitForSeconds(.1f);
            GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(.1f);
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(.1f);
        }

    }

    private IEnumerator SmallDamage()
    {

        yield return new WaitForSeconds(.1f);
        for (int i = 0; i < 2; i++)
        {
            GetComponent<SpriteRenderer>().color = Color.clear;
            yield return new WaitForSeconds(.1f);
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(.1f);
        }

    }

    private void TotalDamage()
    {
        if (healthPoints <= 0)
        {
            healthPoints = 0;
            Instantiate(ExplosionMark, transform.position, Quaternion.identity);
            gameManager.deadEnnemies+=1;
            Destroy(TotalTank);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            healthPoints = healthPoints - 1;
            StartCoroutine(SmallDamage());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerCanonBullet")
        {
            healthPoints = healthPoints - 50;
            Destroy(collision.gameObject);
            StartCoroutine(HeavyDamage());
            getDamage = true;
        }

        if (collision.gameObject.tag == "PlayerMachineGunBullet")
        {
            healthPoints = healthPoints - 2;
            Destroy(collision.gameObject);
            StartCoroutine(SmallDamage());
            getDamage = true;
        }
    }
}
