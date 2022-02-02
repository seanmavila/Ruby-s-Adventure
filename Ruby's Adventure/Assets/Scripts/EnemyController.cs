using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool vertical;
    public float changeTime = 3.0f;
    public ParticleSystem smokeEffect;
    public ParticleSystem hitEffect;

    [SerializeField] float speed;
    Rigidbody2D enemyRb;
    Animator animator;
    float timer;
    int direction = 1;
    bool broken = true;

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
        if (!broken)
        {
            return;
        }
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

    }

    private void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }
        enemyMove();
    }

    public void enemyMove()
    {
        Vector2 position = enemyRb.position;

        if (vertical)
        {
            position.y = position.y +  Time.deltaTime * speed * direction;
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
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
    
    public void Fix()
    {
        broken = false;
        enemyRb.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
        hitEffect.Play();
    }
}
