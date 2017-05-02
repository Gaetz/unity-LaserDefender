using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float Damage;

    public void Hit()
    {
        Destroy(gameObject);
    }
}
