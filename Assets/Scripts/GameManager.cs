using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int deadEnnemies = 0;
    private int totalEnnemies = 4;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(deadEnnemies == totalEnnemies)
        {
            SceneManager.LoadScene("Victory Scene");
        }
	}
}
