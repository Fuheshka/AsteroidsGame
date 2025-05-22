using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public Bullet bulletPrefab;
    public Laser laserPrefab;
    public float thrustSpeed = 1.0f;
    public float turnSpeed = 1.0f;
    private bool thrusting;
    private float turnDirection;

    public int LaserAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 5.0f;
    private bool isReloading = false;
    public TMPro.TMP_Text LaserAmmoText;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentAmmo = LaserAmmo;
        LaserAmmoText.text = currentAmmo.ToString();

    }

    private void OnEnable()
    {
        isReloading = false;
    }

    private void Update()
    {
        thrusting = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));
                 
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            turnDirection = 1.0f;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turnDirection = -1.0f;
        } else
        {
            turnDirection = 0.0f;
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
        if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.X)) && currentAmmo > 0 )
        {
            LaserShoot();
            LaserAmmoText.text = currentAmmo.ToString();
        }

        if (isReloading)
        {
            return;
        }
        if (currentAmmo == 0)
        {
            StartCoroutine(Reload());
            return;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;


        yield return new WaitForSeconds(reloadTime - .25f);
        yield return new WaitForSeconds(1f);


        currentAmmo = LaserAmmo;
        LaserAmmoText.text = currentAmmo.ToString();
        isReloading = false;
    }

    private void FixedUpdate()
    {
        if (thrusting)
        {
            rb.AddForce(this.transform.up * this.thrustSpeed);
        }

        if (turnDirection != 0.0f)
        {
            rb.AddTorque(turnDirection * this.turnSpeed);
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Projectile(this.transform.up);
    }
    private void LaserShoot()
    {
        Laser laser = Instantiate(this.laserPrefab, this.transform.position, this.transform.rotation);
        currentAmmo--;
        LaserAmmoText.text = currentAmmo.ToString();
        laser.ProjectileLaser(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0.0f;
            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }

        if (collision.gameObject.tag == "UFO")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0.0f;
            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}
