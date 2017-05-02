using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    /// <summary>
    /// Maximum horizontal speed
    /// </summary>
    public float MaxSpeedX;

    /// <summary>
    /// Speed increase over time
    /// </summary>
    public float Acceleration;

    /// <summary>
    /// Speed reduction over time
    /// </summary>
    public float Deceleration;

    /// <summary>
    /// Padding for the ship to stop near the screen edge
    /// </summary>
    public float Padding;

    /// <summary>
    /// Horizontal speed
    /// </summary>
    private float speedX;

    /// <summary>
    /// Max player's left position
    /// </summary>
    float xMin;

    /// <summary>
    /// Max player's right position
    /// </summary>
    float xMax;

    // Use this for initialization
    void Start () {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z; 
        xMin = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distanceToCamera)).x + Padding;
        xMax = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distanceToCamera)).x - Padding;
    }

    // Update is called once per frame
    void Update () {
        ManageInput();
        Move();
    }

    /// <summary>
    /// Manage player's input
    /// </summary>
    void ManageInput()
    {
        if(Input.GetKey(KeyCode.LeftArrow) && -MaxSpeedX <= speedX)
        {
            speedX -= Acceleration;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && speedX <= MaxSpeedX)
        {
            speedX += Acceleration;
        }
    }

    /// <summary>
    /// Move player
    /// </summary>
    void Move()
    {
        transform.position += new Vector3(speedX * Time.deltaTime, 0, 0);
        // Keep player in play space
        float clampedX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        // Speed decrease
        DecreaseSpeed();
    }

    /// <summary>
    /// Decrease player's speed overtime
    /// </summary>
    void DecreaseSpeed()
    {
        if (-Deceleration <= speedX && speedX <= Deceleration)
        {
            speedX = 0;
            return;
        }
        if (speedX > 0)
        {
            speedX -= Deceleration;
        }
        else if (speedX < 0)
        {
            speedX += Deceleration;
        }
    }
}
