using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject EnemyPrefab;
    public float Width;
    public float Height;
    public float SpeedX;

    /// <summary>
    /// Padding for the ship to stop near the screen edge
    /// </summary>
    private float padding;

    /// <summary>
    /// True when moving right
    /// </summary>
    private bool isMovingRight;

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
        // Screen
        padding = Width / 2;
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        xMin = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distanceToCamera)).x + padding;
        xMax = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distanceToCamera)).x - padding;
        // Generating
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(EnemyPrefab, child.transform.position, Quaternion.identity);
            enemy.transform.parent = child;
        }
	}

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height));
    }
	
	// Update is called once per frame
	void Update () {
        // Move
		if(isMovingRight)
        {
            transform.position += Vector3.right * SpeedX * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * SpeedX * Time.deltaTime;
        }        
        // Keep player in play space
        float clampedX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        // Turn player
        if(transform.position.x <= xMin || transform.position.x >= xMax)
        {
            isMovingRight = !isMovingRight;
        }
    }
}
