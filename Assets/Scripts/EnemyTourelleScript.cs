using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTourelleScript : MonoBehaviour {

    [SerializeField] private Transform enemyTank;
    [SerializeField] private Transform emptyObject;
    Vector2 direction;
    

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = enemyTank.position;
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position) - transform.position);
        direction = emptyObject.position - enemyTank.position;
        var hit = Physics2D.Raycast(enemyTank.position, direction, 5f, 0);
        Debug.DrawRay(enemyTank.position, direction, Color.magenta, 0.1f);
        
    }

    public void TotalDamage()
    {
            Destroy(gameObject);
            Destroy(this);
    }
}
