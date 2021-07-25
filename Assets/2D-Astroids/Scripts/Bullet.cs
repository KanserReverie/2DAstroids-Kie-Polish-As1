using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D myRB;

    public float speed = 600f;

    public float maxLifetime = 10.0f;

    private void Awake()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        myRB.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
    }
}