using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool vertical;
    public float changeTime = 3.0f;

    [SerializeField] private float speed;
    private Rigidbody2D enemyRb;
    private Animator animator;
    private float timer;
    private int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

    }

    private void FixedUpdate()
    {
        enemyMove();
    }

    public void enemyMove()
    {
        Vector2 position = enemyRb.position;

        if (vertical)
        {
            position.y += Time.deltaTime * speed * direction;
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", direction);
        }
        else
        {
            position.x += Time.deltaTime * speed * direction;
            animator.SetFloat("moveX", direction);
            animator.SetFloat("moveY", 0);
        }
        enemyRb.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}
