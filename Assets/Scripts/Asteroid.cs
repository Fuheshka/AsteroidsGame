using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    public float Size = 1.0f;
    public float minumumSize = 0.5f;
    public float maximumSize = 1.5f;
    public float asteroidSpeed = 50.0f;
    public float MaxAsteroidLife = 30.0f;
    private SpriteRenderer spr;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        spr.sprite = sprites[Random.Range(0, sprites.Length)];
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.Size;

        rb.mass = this.Size;

        
    }

    public void SetTrajectory(Vector2 direction)
    {
        rb.AddForce(direction * this.asteroidSpeed);

        Destroy(this.gameObject, this.MaxAsteroidLife);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            if ((this.Size * 0.5f) >= this.minumumSize)
            {
                CreateSplit();
                CreateSplit();
            }

            FindAnyObjectByType<GameManager>().AsteroidDestroyed(this);

            Destroy(this.gameObject);

        }
        if(collision.gameObject.tag == "Laser")
        {
            FindAnyObjectByType<GameManager>().AsteroidDestroyed(this);
            Destroy(this.gameObject);
        }
    }

    private void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;
        Asteroid HalfAsteroid = Instantiate(this, position, this.transform.rotation);
        HalfAsteroid.Size = this.Size * 0.5f;
        HalfAsteroid.SetTrajectory(Random.insideUnitCircle.normalized * this.asteroidSpeed);
    }
}
