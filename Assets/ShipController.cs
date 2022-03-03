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
        Debug.Log("thrust forward");

        rigidBody.AddRelativeForce(Vector2.up * this.forwardThrustForce);
    }

    public void turnLeft()
    {

    }

    public void turnRight()
    {

    }
}
