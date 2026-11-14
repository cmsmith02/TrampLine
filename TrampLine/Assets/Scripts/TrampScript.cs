using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampScript : MonoBehaviour
{
    private float angle;

    public void Start()
    {
        angle = transform.rotation.eulerAngles.z;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 velocity = -collision.relativeVelocity;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.Reflect(velocity, new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle)));
        }
    }
}
