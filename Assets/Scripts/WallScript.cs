using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {

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
