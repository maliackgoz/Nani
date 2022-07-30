using System;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    private void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        //transform.Rotate(0,0,2);
    }

    private void OnCollisionEnter2D(Collision2D collision) // collision can not be changed
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit an enemy!");
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Platform") | col.gameObject.CompareTag("Boundary"))
        {
            Destroy(this.gameObject);
        }
    }
}