using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float bulletSpeed = 50.0f;
    public float MaxBulletLife = 10.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Projectile(Vector2 direction)
    {
        rb.AddForce(direction * this.bulletSpeed);
        Destroy(this.gameObject, this.MaxBulletLife);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
