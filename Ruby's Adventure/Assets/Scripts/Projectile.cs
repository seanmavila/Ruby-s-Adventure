using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D projectileRb;

    // Start is called before the first frame update
    void Awake()
    {
        projectileRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        projectileRb.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemyRb = collision.collider.GetComponent<EnemyController>();
        if (enemyRb != null)
        {
            enemyRb.Fix();
        }
        Destroy(gameObject);
    }
}
