using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour {

    [SerializeField] private Vector3 targetScale;
    public GameObject pressGPanel;
    [SerializeField] private Vector3 baseScale;
    float speed = 100.0f;
    bool closeEnough = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (closeEnough)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                transform.localScale = Vector3.Lerp(transform.localScale, targetScale, speed * Time.deltaTime);
                pressGPanel.SetActive(false);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pressGPanel.SetActive(true);
            closeEnough = true;
        }

        if (collision.gameObject.tag == "PlayerCanonBullet")
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "PlayerMachineGunBullet")
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "EnemyCanonBullet")
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "EnemyHelicopterBullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
