using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private Rigidbody2D body;
    public float speed;
    public float turnSpeed;

    private string movementAxisName;
    private string turnAxisName;
    private float movementInputValue;
    private float turnInputValue;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        body.isKinematic = false;
        movementInputValue = 0f;
        turnInputValue = 0f;
    }

    void OnDisable()
    {
        body.isKinematic = true;
    }

    void Start()
    {
        movementAxisName = "Vertical";
        turnAxisName = "Horizontal";
    }

    void Update()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            
        }
        movementInputValue = Input.GetAxis(movementAxisName);
            turnInputValue = Input.GetAxis(turnAxisName);
        Turn();
        Move();
    }

    private void Move()
    {
        Vector2 movement = transform.up * movementInputValue * speed * Time.deltaTime;
        body.MovePosition(body.position + movement);
    }

    private void Turn()
    {
    transform.Rotate(-Vector3.forward* turnInputValue * turnSpeed* Time.deltaTime);
    }

    private void JoystickControls()
    {

    }
}
