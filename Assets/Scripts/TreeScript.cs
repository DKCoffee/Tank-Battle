using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator Flash()
    {

        GetComponent<SpriteRenderer>().color = Color.clear;
        yield return new WaitForSeconds(.1f);
        for (int i = 0; i < 5; i++)
        {
            GetComponent<SpriteRenderer>().color = Color.clear;
            yield return new WaitForSeconds(.2f);

            GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(.2f);
        }
        
    }

    private void GetDestroy()
    {
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Flash();
            GetDestroy();
        }

        if (collision.gameObject.tag == "EnemyTank")
        {
            GetDestroy();
        }
    }
}
