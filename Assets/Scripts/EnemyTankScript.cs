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
    [SerializeField] public GameObject waypoint;
    private float maximumSpawnDistance = 5;
    [SerializeField] int gridSizeX, gridSizeY;


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
        SEARCH,
        GO_BACK
    }
    private EnemyState enemyState = EnemyState.PATROL;

    private float distance;
    private Transform playerTransform;
    private float originOffset = 0.5f;
    public float raycastMaxDistance = 10f;
    private EnemyTourelleScript enemyTourelleScript;

    // Use this for initialization
    void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        playerTransform = FindObjectOfType<PlayerScript>().transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyTourelleScript = FindObjectOfType<EnemyTourelleScript>();
        healthPoints = maxHealthPoints;
        GenerateWaypoints(4);
    }
	
	// Update is called once per frame
	void Update ()
    {
        distance = Vector2.Distance(transform.position, playerTransform.transform.position);
        if (distance <= 10)
        {
            enemyState = EnemyState.FOLLOW;
        }
        if (distance <=5)
        {
            enemyState = EnemyState.ATTACK;
        }
        var hit = Physics2D.Raycast(transform.position, transform.forward, 5f, 0);
        Debug.DrawRay(transform.position, transform.forward, Color.green, 0.1f);
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

                break;
            case EnemyState.ATTACK:

                break;
            case EnemyState.SEARCH:

                break;
            case EnemyState.GO_BACK:

                break;
        }
    }

    private void GenerateWaypoints(int waypoints)
    {
        int CountEnemy = 0;
        while (CountEnemy != waypoints)
        {
            GameObject[,] generared_map = map_generator.Generared_map;

            int _x = Mathf.RoundToInt((9646 * Random.value + 5947) % (gridSizeX - 1));
            int _y = Mathf.RoundToInt((9646 * Random.value + 5947) % (gridSizeY - 1));
            if (generared_map[_x, _y].layer != 8)//CHIFFRE MAGIQUE
            {
                float distance = Vector3.Distance(transform.position, generared_map[_x, _y].transform.position);

                if (distance <= maximumSpawnDistance)
                {
                    if (CountEnemy != waypoints)
                    {
                        Instantiate(waypoint, generared_map[_x, _y].transform.position, Quaternion.identity);
                        CountEnemy++;
                    }

                }
            }
        }
    }

    private void TotalDamage()
    {
        if (healthPoints <= 0)
        {
            healthPoints = 0;
            GetComponent<SpriteRenderer>().color = Color.grey;
            Destroy(GetComponent<BoxCollider>());
            enemyTourelleScript.TotalDamage();
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
