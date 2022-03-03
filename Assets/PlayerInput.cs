using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public ShipController shipController;

    // Start is called before the first frame update
    void Start()
    {
        shipController = GetComponent<ShipController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") > 0) {
            shipController.thrustForward();
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            shipController.thrustBackward();    
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            shipController.turnLeft();
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            shipController.turnRight();
        }
    }
}
