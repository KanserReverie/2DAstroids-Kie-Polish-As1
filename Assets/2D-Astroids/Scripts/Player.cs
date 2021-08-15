using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // This will be a prefab of the bullet.
    public Bullet bulletPrefab;

    // Turning and moving speed of the ship.
    [Header("Ship Movement")]
    public float thrustSpeed = 1.0f;
    public float turnSpeed = 1.0f;

    // These are just to hold the movement of the ship itself.
    private Rigidbody2D _myRb;
    private bool _thrusting;
    private float _turningDirection;

    // Used to find all references.
    private void Awake()
    {
        _myRb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        // Used to get the inputs of the player.
        GetInputs();
    }

    // Used for moving, turning and shooting.
    private void GetInputs()
    {
        // Thrusing is a bool to move foward.
        _thrusting = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));

        // If pressing Left or Right move that direction.
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _turningDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _turningDirection = -1.0f;
        }
        // If nothing is pushed don't put any force on the ship
        else
        {
            _turningDirection = 0f;
        }

        // If space is pressed shoot.
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    // FixedUpdate is used for physics as it is called at a constant rate no matter framerate.
    private void FixedUpdate()
    {
        // if moving foward, move foward
        if (_thrusting)
        {
            _myRb.AddForce(this.transform.up * this.thrustSpeed);
        }

        if (_turningDirection != 0.0f)
        {
            _myRb.AddTorque(_turningDirection * this.turnSpeed);
        }

    }

    public GameObject Shoot()
    {
        var transform1 = transform;
        Bullet bullet = Instantiate(bulletPrefab, transform1.position, transform1.rotation);
        bullet.Project(transform.up);
        return bullet.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            _myRb.velocity = Vector3.zero;
            _myRb.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            GameManager.instance.PlayerDied();
        }
    }
}