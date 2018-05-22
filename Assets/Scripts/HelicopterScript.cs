using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterScript : MonoBehaviour {

    private Rigidbody2D body;
    [SerializeField] private GameObject propeller;
    private Transform playerTransform;
    [SerializeField] private float speed;
    private Transform startPosition;
    private Transform target;
    private float distance;

    [Header("Machine Gun Settings")]
    public float machineGunBulletSpeed;
    public GameObject machineGunBulletPrefab;
    public Transform machineGunBulletSpawn;
    private float lastTimeShoot;
    public float timeToShoot;

    [Header("Health Settings")]
    public float healthPoints;
    private float maxHealthPoints = 50;

    [SerializeField] private Transform[] wayPoints;
    private Transform nextPosition;
    private float minimumValue = 0.1f;
    [SerializeField] private GameObject ExplosionMark;
    private GameManager gameManager;

    public enum EnemyState
    {
        PATROL,
        FOLLOW,
        ATTACK,
        GO_BACK
    }
    private EnemyState enemyState = EnemyState.PATROL;

    // Use this for initialization
    void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        playerTransform = FindObjectOfType<PlayerScript>().transform;
        gameManager = FindObjectOfType<GameManager>();
        healthPoints = maxHealthPoints;
        startPosition = transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        propeller.transform.Rotate(0, 0, 2000 * Time.deltaTime);
        AI();
        distance = Vector2.Distance(transform.position, playerTransform.transform.position);
        if (distance <= 15)
        {
            enemyState = EnemyState.FOLLOW;
        }
        if (distance <= 5)
        {
            enemyState = EnemyState.ATTACK;
        }
        if (distance >= 20)
        {
            enemyState = EnemyState.GO_BACK;
        }
    }

    private void MachineGunShoot()
    {
        if (Time.realtimeSinceStartup - lastTimeShoot > timeToShoot)
        {
            GameObject bullet = Instantiate(machineGunBulletPrefab, machineGunBulletSpawn.position, machineGunBulletSpawn.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = machineGunBulletSpawn.up * machineGunBulletSpeed;
            Destroy(bullet, 2);
            lastTimeShoot = Time.realtimeSinceStartup;
        }

    }

    private void Move()
    {
        target = playerTransform;
        transform.position += transform.up * speed * Time.deltaTime;
        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
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
                MachineGunShoot();
                break;
            case EnemyState.GO_BACK:
                target = startPosition;
                Move();
                break;
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
            gameManager.deadEnnemies++;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerCanonBullet")
        {
            healthPoints = healthPoints - 50;
            Destroy(collision.gameObject);
            TotalDamage();
        }

        if (collision.gameObject.tag == "PlayerMachineGunBullet")
        {
            healthPoints = healthPoints - 5;
            Destroy(collision.gameObject);
            StartCoroutine(SmallDamage());
            TotalDamage();
        }
    }
}
