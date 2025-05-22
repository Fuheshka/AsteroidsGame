using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public float Size = 1.0f;
    public float minumumSize = 0.5f;
    public float maximumSize = 1.5f;
    public float UFOSpeed = 50.0f;
    public float MaxUFOLife = 30.0f;
    private SpriteRenderer spr;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        this.transform.localScale = Vector3.one * this.Size;

        rb.mass = this.Size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        rb.AddForce(direction * this.UFOSpeed);

        Destroy(this.gameObject, this.MaxUFOLife);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {

            FindAnyObjectByType<GameManager>().UFODestroyed(this);

            Destroy(this.gameObject);

        }
        if (collision.gameObject.tag == "Laser")
        {
            FindAnyObjectByType<GameManager>().UFODestroyed(this);
            Destroy(this.gameObject);
        }
    }
}
