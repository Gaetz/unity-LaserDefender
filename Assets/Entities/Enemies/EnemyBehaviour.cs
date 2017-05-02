using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    /// <summary>
    /// Enemy health
    /// </summary>
    public float Health;

    /// <summary>
    /// Enemy laser
    /// </summary>
    public GameObject Projectile;

    /// <summary>
    /// Laser speed
    /// </summary>
    public float ProjectileSpeed;

    /// <summary>
    /// Laser speed
    /// </summary>
    public float ProjectileProbability;

    /// <summary>
    /// Score value when enemy dies
    /// </summary>
    public int ScoreValue;

    /// <summary>
    /// Reference to the score keeper to call points increase
    /// </summary>
    private ScoreKeeper scoreKeeper;

    /// <summary>
    /// Sound when fire
    /// </summary>
    public AudioClip FireSound;

    /// <summary>
    /// Sound when dies
    /// </summary>
    public AudioClip DeathSound;

    /// <summary>
    /// Offset for creating projectile in front of ^space ship
    /// </summary>
    Vector3 ProjectileOffset = new Vector3(0, -0.8f, 0);

    /// <summary>
    /// On creation
    /// </summary>
    void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    /// <summary>
    /// Behaviour when collided by a laser
    /// </summary>
    /// <param name="collider">The laser that collides</param>
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Handle projectile hit
        Projectile projectile = collider.gameObject.GetComponent<Projectile>();
        if(projectile)
        {
            Health -= projectile.Damage;
            projectile.Hit();
        }
        // Death
        if(Health <= 0)
        {
            AudioSource.PlayClipAtPoint(DeathSound, transform.position);
            scoreKeeper.ScorePoints(ScoreValue);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        float probability = Time.deltaTime * ProjectileProbability;
        if (Random.value < probability)
            Fire();
    }

    void Fire()
    {
        GameObject beam = Instantiate(Projectile, transform.position + ProjectileOffset, Quaternion.identity);
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -ProjectileSpeed, 0);
        AudioSource.PlayClipAtPoint(FireSound, transform.position);
    }
}
