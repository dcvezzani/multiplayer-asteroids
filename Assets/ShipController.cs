using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class ShipController : MonoBehaviour
{
    public float forwardThrustForce;
    public float backwardThrustForce;
    public float rotationThrustForce;

    public Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void thrustForward()
    {
        rigidBody.AddRelativeForce(Vector2.up * forwardThrustForce);
    }
    public void thrustBackward()
    {
        rigidBody.AddRelativeForce(Vector2.down * backwardThrustForce);
    }

    public void turnLeft()
    {
        rigidBody.AddTorque(rotationThrustForce);
    }

    public void turnRight()
    {
        rigidBody.AddTorque(-rotationThrustForce);
    }
}
