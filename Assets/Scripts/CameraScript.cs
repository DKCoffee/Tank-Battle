using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform target;
    public Vector3 offset;
    public float speed = 0.01f;

    // Use this for initialization
    void Start () {
		target = FindObjectOfType<PlayerScript>().transform;
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed);
        transform.position = smoothedPosition;
    }
}
