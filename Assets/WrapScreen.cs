using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class WrapScreen : MonoBehaviour
{
    private float xmin, xmax;
    private float ymin, ymax;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 viewPortDimensions = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        xmin = viewPortDimensions.x / -1;
        xmax = viewPortDimensions.x / 1;
        ymin = viewPortDimensions.y / -1;
        ymax = viewPortDimensions.y / 1;

        //Debug.Log("view port dimensions: " + xmin + ", " + xmax + ", " + ymin + ", " + ymax);

        // cache dimensions for the sprite
        // include in update logic below
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < xmin)
        {
            transform.position = new Vector2(xmax, transform.position.y);
        }
        else if (transform.position.x > xmax)
        {
            transform.position = new Vector2(xmin, transform.position.y);
        }
        else if (transform.position.y < ymin)
        {
            transform.position = new Vector2(transform.position.x, ymax);
        }
        else if (transform.position.y > ymax)
        {
            transform.position = new Vector2(transform.position.x, ymin);
        }
    }
}
