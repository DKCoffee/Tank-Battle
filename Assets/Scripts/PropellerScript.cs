using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerScript : MonoBehaviour {
    [SerializeField] private Transform helicopter;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = helicopter.position;
        transform.Rotate(0, 0, 2000 * Time.deltaTime);
    }
}
