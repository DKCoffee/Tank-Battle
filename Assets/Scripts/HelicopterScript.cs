using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterScript : MonoBehaviour {

    [SerializeField] private GameObject propeller;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        propeller.transform.Rotate(0, 0, 2000 * Time.deltaTime);
    }
}
