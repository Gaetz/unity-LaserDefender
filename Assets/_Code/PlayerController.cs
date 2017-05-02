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
    /// Horizontal speed
    /// </summary>
    private float speedX;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ManageInput();
        Move();
    }

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

    void Move()
    {
        transform.position += new Vector3(speedX * Time.deltaTime, 0, 0);
        DecreaseSpeed();
    }

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
