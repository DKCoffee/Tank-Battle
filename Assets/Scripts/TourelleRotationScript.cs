using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourelleRotationScript : MonoBehaviour {

    [SerializeField] private Transform player;
    [SerializeField] private Transform emptyObject;
    Vector2 direction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        //float dirX = Input.GetAxis("Horizontal");
        //float dirY = Input.GetAxis("Vertical");
        //float angle = Mathf.Atan2(dirY, dirX) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //Vector3 pos1 = Camera.main.WorldToScreenPoint(transform.position);
        //Vector3 dir = Input.mousePosition - pos1;
        //float angle1 = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle1, Vector3.forward);
        Debug.Log(direction);
        transform.position = player.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position) - transform.position);
        direction = emptyObject.position - player.position;
        var hit = Physics2D.Raycast(player.position, transform.forward, 5f, 0);
        Debug.DrawRay(player.position, direction, Color.magenta, 0.1f);
    }
}
