using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator Flash()
    {
        yield return new WaitForSeconds(.1f);
        for (int i = 0; i < 5; i++)
        {
            GetComponent<SpriteRenderer>().color = Color.clear;
            yield return new WaitForSeconds(.1f);
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(.1f);
        }
        
    }

    private void GetDestroy()
    {
        Destroy(gameObject, 0.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Flash());
            GetDestroy();
        }

        if (collision.gameObject.tag == "EnemyTank")
        {
            StartCoroutine(Flash());
            GetDestroy();
        }
    }
}
