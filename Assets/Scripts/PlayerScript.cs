using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private Rigidbody2D body;
    [SerializeField]private Transform emptyObject;
    private Vector2 direction;
    public float speed;
    public float turnSpeed;

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
            GameObject Snowball = Instantiate(machineGunBulletPrefab, machineGunBulletSpawn.position, machineGunBulletSpawn.rotation);
            Snowball.GetComponent<Rigidbody2D>().velocity = machineGunBulletSpawn.up * machineGunBulletSpeed;
            Destroy(Snowball, 2);
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
}
