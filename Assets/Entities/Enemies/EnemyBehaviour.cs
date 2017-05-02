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

    Vector3 ProjectileOffset = new Vector3(0, -0.8f, 0);

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
            Destroy(gameObject);
        }
    }

    void Update()
    {
        float probability = Time.deltaTime * ProjectileProbability;
        if(Random.value < probability)
            Fire();
    }

    void Fire()
    {
        GameObject beam = Instantiate(Projectile, transform.position + ProjectileOffset, Quaternion.identity);
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -ProjectileSpeed, 0);
    }
}
