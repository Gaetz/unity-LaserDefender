using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public float Health;

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
}
