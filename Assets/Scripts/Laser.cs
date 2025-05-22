using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private Rigidbody2D rb;
    public float LaserSpeed = 800.0f;
    public float MaxLaserLife = 10.0f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

 
    public void ProjectileLaser(Vector2 direction)
    {
        rb.AddForce(direction * this.LaserSpeed);
        Destroy(this.gameObject, this.MaxLaserLife);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}