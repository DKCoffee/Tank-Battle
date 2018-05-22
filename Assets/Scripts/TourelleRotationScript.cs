using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourelleRotationScript : MonoBehaviour {

    [SerializeField] private Transform player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = player.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position) - transform.position);
    }
}
