using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankScript : MonoBehaviour {

    private Rigidbody2D body;
    public float speed;
    public float turnSpeed;

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

    // Use this for initialization
    void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        playerTransform = FindObjectOfType<PlayerScript>().transform;
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
    }

    private void Move()
    {
        Vector2 movement = transform.up * speed * Time.deltaTime;
        body.MovePosition(body.position + movement);
    }

    private void Turn()
    {
        transform.Rotate(-Vector3.forward * turnSpeed * Time.deltaTime);
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
}
