using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EZCameraShake;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D body;
    [SerializeField] private Transform emptyObject;
    private Vector2 direction;
    public float speed;
    public float turnSpeed;
    private SpriteRenderer spriteRenderer;

    private string movementAxisName;
    private string turnAxisName;
    private float movementInputValue;
    private float turnInputValue;

    [Header("Machine Gun Settings")]
    public float machineGunBulletSpeed;
    public GameObject machineGunBulletPrefab;
    public Transform machineGunBulletSpawn;
    public int FireRate;
    public int lastfired;

    [Header("Canon Settings")]
    public float canonBulletSpeed;
    public GameObject canonBulletPrefab;
    public Transform canonBulletSpawn;
    private float lastTimeShoot;
    public float timeToShoot;

    [Header("Health Settings")]
    public float healthPoints;
    private float maxHealthPoints = 100;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        body.isKinematic = false;
        movementInputValue = 0f;
        turnInputValue = 0f;
    }

    void OnDisable()
    {
        body.isKinematic = true;
    }

    void Start()
    {
        movementAxisName = "Vertical";
        turnAxisName = "Horizontal";
        healthPoints = maxHealthPoints;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            CanonShoot();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            StartCoroutine("AutomaticShoot");
        }
        if (Input.GetButtonUp("Fire2"))
        {
            StopCoroutine("AutomaticShoot");
        }
        movementInputValue = Input.GetAxis(movementAxisName);
        turnInputValue = Input.GetAxis(turnAxisName);
        Turn();
        Move();
        direction = emptyObject.position - transform.position;
        var hit = Physics2D.Raycast(transform.forward, direction, 5f, 0);
        Debug.DrawRay(transform.forward, direction, Color.yellow, 0.1f);
    }

    private void Move()
    {
        Vector2 movement = transform.up * movementInputValue * speed * Time.deltaTime;
        body.MovePosition(body.position + movement);
    }

    private void Turn()
    {
        transform.Rotate(-Vector3.forward * turnInputValue * turnSpeed * Time.deltaTime);
    }

    private void MachineGunShoot()
    {
        GameObject Bullet = Instantiate(machineGunBulletPrefab, machineGunBulletSpawn.position, machineGunBulletSpawn.rotation);
        Bullet.GetComponent<Rigidbody2D>().velocity = machineGunBulletSpawn.up * machineGunBulletSpeed;
        Destroy(Bullet, 2);
        CameraShaker.Instance.ShakeOnce(.5f, 1f, .1f, 1f);
    }

    private void CanonShoot()
    {
        if (Time.realtimeSinceStartup - lastTimeShoot > timeToShoot)
        {
            GameObject Bullet = Instantiate(canonBulletPrefab, canonBulletSpawn.position, canonBulletSpawn.rotation);
            Bullet.GetComponent<Rigidbody2D>().velocity = canonBulletSpawn.up * canonBulletSpeed;
            Destroy(Bullet, 5);
            lastTimeShoot = Time.realtimeSinceStartup;
            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        }
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
        if(healthPoints <= 0)
        {
            healthPoints = 0;
            SceneManager.LoadScene("LooseMenu");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyTank")
        {
            healthPoints = healthPoints - 1;
            StartCoroutine(SmallDamage());
            CameraShaker.Instance.ShakeOnce(2f, 2f, .1f, 1f);
        }

        if (collision.gameObject.tag == "Tree")
        {
            healthPoints = healthPoints - 1;
            StartCoroutine(SmallDamage());
            CameraShaker.Instance.ShakeOnce(2f, 2f, .1f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyCanonBullet")
        {
            healthPoints = healthPoints - 25;
            StartCoroutine(HeavyDamage());
            Destroy(collision.gameObject);
            CameraShaker.Instance.ShakeOnce(5f, 5f, .1f, 1f);
        }

        if (collision.gameObject.tag == "EnemyHelicopterBullet")
        {
            healthPoints = healthPoints - 5;
            StartCoroutine(HeavyDamage());
            Destroy(collision.gameObject);
            CameraShaker.Instance.ShakeOnce(.5f, .5f, .1f, 1f);
        }
    }
}
