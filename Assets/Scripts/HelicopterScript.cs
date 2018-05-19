using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterScript : MonoBehaviour {

    [SerializeField] private GameObject propeller;
    private Transform playerTransform;
    [SerializeField] private float speed;
    private Transform startPosition;
    private Transform target;

    [Header("Machine Gun Settings")]
    public float machineGunBulletSpeed;
    public GameObject machineGunBulletPrefab;
    public Transform machineGunBulletSpawn;
    public int FireRate;
    public int lastfired;

    [Header("Health Settings")]
    public float healthPoints;
    private float maxHealthPoints = 50;

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
        playerTransform = FindObjectOfType<PlayerScript>().transform;
        healthPoints = maxHealthPoints;
        startPosition = transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log("helicopter " + healthPoints);
        propeller.transform.Rotate(0, 0, 2000 * Time.deltaTime);
        Move();
        MachineGunShoot();
    }

    private void MachineGunShoot()
    {
        GameObject Snowball = Instantiate(machineGunBulletPrefab, machineGunBulletSpawn.position, machineGunBulletSpawn.rotation);
        Snowball.GetComponent<Rigidbody2D>().velocity = machineGunBulletSpawn.up * machineGunBulletSpeed;
        Destroy(Snowball, 2);
    }

    IEnumerator AutomaticShoot()
    {
        while (true)
        {
            MachineGunShoot();
            yield return new WaitForSeconds(0.1f);
            MachineGunShoot();
            yield return new WaitForSeconds(0.1f);
            MachineGunShoot();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Move()
    {
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
                StartCoroutine("AutomaticShoot");
                break;
            case EnemyState.GO_BACK:
                target = startPosition;
                break;
        }
    }

    private void TotalDamage()
    {
        if (healthPoints <= 0)
        {
            healthPoints = 0;
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
            TotalDamage();
        }
    }
}
