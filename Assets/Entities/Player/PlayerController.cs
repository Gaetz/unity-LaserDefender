﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    /// <summary>
    /// Player's Health
    /// </summary>
    public float Health;

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
    /// Laser the player shot
    /// </summary>
    public GameObject Projectile;

    /// <summary>
    /// Laser speed
    /// </summary>
    public float ProjectileSpeed;

    /// <summary>
    /// Laser speed
    /// </summary>
    public float ProjectileCooldown;

    /// <summary>
    /// Sound when fire
    /// </summary>
    public AudioClip FireSound;

    /// <summary>
    /// Projectile offset ofr not hitting oneself
    /// </summary>
    private Vector3 projectileOffset = new Vector3(0, 0, 0);

    /// <summary>
    /// Horizontal speed
    /// </summary>
    float speedX;

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
        // Move
        if(Input.GetKey(KeyCode.LeftArrow) && -MaxSpeedX <= speedX)
        {
            speedX -= Acceleration;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && speedX <= MaxSpeedX)
        {
            speedX += Acceleration;
        }
        // Laser
        if(Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.0001f, ProjectileCooldown);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
    }

    /// <summary>
    /// Fire a laser
    /// </summary>
    void Fire()
    {
        GameObject beam = Instantiate(Projectile, transform.position + projectileOffset, Quaternion.identity);
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, ProjectileSpeed, 0);
        AudioSource.PlayClipAtPoint(FireSound, transform.position);
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

    /// <summary>
    /// Behaviour when collided by a laser
    /// </summary>
    /// <param name="collider">The laser that collides</param>
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Handle projectile hit
        Projectile projectile = collider.gameObject.GetComponent<Projectile>();
        if (projectile)
        {
            Health -= projectile.Damage;
            projectile.Hit();
        }
        // Death
        if (Health <= 0)
        {
            Lose();
        }
    }

    /// <summary>
    /// Called when player loses
    /// </summary>
    void Lose()
    {
        LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levelManager.LoadLevel("WinScreen");
        Destroy(gameObject);
    }
}
