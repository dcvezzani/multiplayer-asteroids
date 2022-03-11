using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class ShipController : MonoBehaviour
{
    public float forwardThrustForce;
    public float backwardThrustForce;
    public float rotationThrustForce;

    public ThrustEffect backCenterThrust;
    public ThrustEffect backRightThrust;
    public ThrustEffect backLeftThrust;
    public ThrustEffect frontRightThrust;
    public ThrustEffect frontLeftThrust;

    private float velTemp = 0f;
    private float accTemp = 0f;

    public Rigidbody2D rigidBody;
    public SpriteRenderer shipShading;

    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        backCenterThrust.HideEffect();
        backRightThrust.HideEffect();
        backLeftThrust.HideEffect();
        frontRightThrust.HideEffect();
        frontLeftThrust.HideEffect();
    }

    // Update is called once per frame
    private void FixedUpdate() //Fixed Update for physics calcualtions
    {
        //Calculate Forward Local Acceleration
        float forwardLocalVelocity = Vector2.Dot(rigidBody.velocity, rigidBody.gameObject.transform.up);
        float localAcceleration;
        float buffer = 0.04f;

        if (forwardLocalVelocity > velTemp + buffer) { localAcceleration = 1; } else if (forwardLocalVelocity < velTemp - buffer) { localAcceleration = -1; } else { localAcceleration = 0; }

        velTemp = forwardLocalVelocity;

        //Calculate Angular Acceleration
        float angularVel = rigidBody.angularVelocity;
        float localAngularAcceleration;
        buffer = 8f;

        if (angularVel > accTemp + buffer) { localAngularAcceleration = 1; } else if (angularVel < accTemp - buffer) { localAngularAcceleration = -1; } else { localAngularAcceleration = 0; }

        accTemp = angularVel;

        //Debug.Log("Forward Local Velocity:" + forwardLocalVelocity + ", Angular Velocity: " + rigidBody.angularVelocity + ", Local Acceleration: " + localAcceleration + ", Angular Acceleration: " + localAngularAcceleration);

        if (localAcceleration > 0)
        {
            if (backCenterThrust.isShowingEffect == false) { backCenterThrust.ShowEffect(); }
        }
        else
        {
            if (backCenterThrust.isShowingEffect == true) { backCenterThrust.HideEffect(); }
        }

        if (localAcceleration < 0)
        {
            if (frontLeftThrust.isShowingEffect == false) { frontLeftThrust.ShowEffect(); }
            if (frontRightThrust.isShowingEffect == false) { frontRightThrust.ShowEffect(); }
        }
        else
        {
            if (frontLeftThrust.isShowingEffect == true) { frontLeftThrust.HideEffect(); }
            if (frontRightThrust.isShowingEffect == true) { frontRightThrust.HideEffect(); }
        }

        if (localAngularAcceleration > 0)
        {
            if (frontLeftThrust.isShowingEffect == false) { frontLeftThrust.ShowEffect(); }
            if (backRightThrust.isShowingEffect == false) { backRightThrust.ShowEffect(); }
            if (frontRightThrust.isShowingEffect == true) { frontRightThrust.HideEffect(); }
            if (backLeftThrust.isShowingEffect == true) { backLeftThrust.HideEffect(); }
        }
        else if (localAngularAcceleration < 0)
        {
            if (frontRightThrust.isShowingEffect == false) { frontRightThrust.ShowEffect(); }
            if (backLeftThrust.isShowingEffect == false) { backLeftThrust.ShowEffect(); }
            if (frontLeftThrust.isShowingEffect == true) { frontLeftThrust.HideEffect(); }
            if (backRightThrust.isShowingEffect == true) { backRightThrust.HideEffect(); }
        }

        if (localAngularAcceleration == 0)
        {
            if (backRightThrust.isShowingEffect == true) { backRightThrust.HideEffect(); }
            if (backLeftThrust.isShowingEffect == true) { backLeftThrust.HideEffect(); }
        }

        if (localAcceleration == 0 && localAngularAcceleration == 0)
        {
            if (frontLeftThrust.isShowingEffect == true) { frontLeftThrust.HideEffect(); }
            if (frontRightThrust.isShowingEffect == true) { frontRightThrust.HideEffect(); }
        }
    }

    private void Update() //Added 3d looking shading effect to sprite
    {
        float transparency = 1f;
        float fadeBuffer = 35f;
        if (transform.localEulerAngles.z > (315 - fadeBuffer) && transform.localEulerAngles.z < (315 + fadeBuffer))
        {
            transparency = (transform.localEulerAngles.z - 315 + fadeBuffer) / (fadeBuffer * 2);
        }
        else if (transform.localEulerAngles.z > (135 - fadeBuffer) && transform.localEulerAngles.z < (135 + fadeBuffer))
        {
            transparency = 1 - ((transform.localEulerAngles.z - 135 + fadeBuffer) / (fadeBuffer * 2));
        }
        else if (transform.localEulerAngles.z > 135 + fadeBuffer && transform.localEulerAngles.z < 315 - fadeBuffer)
        {
            transparency = 0;
        }
        else
        {
            transparency = 1;
        }

        //Debug.Log("Transparency: " + transparency + ", angle: " + transform.localEulerAngles.z);
        shipShading.color = new Color(1, 1, 1, transparency);
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
