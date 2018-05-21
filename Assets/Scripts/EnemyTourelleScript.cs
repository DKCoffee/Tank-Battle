using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTourelleScript : MonoBehaviour {

    [SerializeField] private Transform enemyTank;
    [SerializeField] private Transform emptyObject;
    private Transform playerTransform;
    Vector2 direction;
    

    // Use this for initialization
    void Start () {
        playerTransform = FindObjectOfType<PlayerScript>().transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = enemyTank.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, playerTransform.position - transform.position);
    }

}
