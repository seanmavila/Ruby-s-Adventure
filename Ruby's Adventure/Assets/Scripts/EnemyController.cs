using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public bool vertical;
    public float changeTime = 3.0f;
    public ParticleSystem smokeEffect;
    public ParticleSystem hitEffect;
    private Transform player;

    [SerializeField] float speed;
    Rigidbody2D enemyRb;
    Animator animator;
    float timer;
    int direction = 1;
    bool broken = true;
    int aggroRange = 3;
    bool isAggro = false;
    private Vector2 movement;
    private Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timer = changeTime;
        scoreText = GameObject.Find("Score Text").GetComponent<Text>();
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
        Vector3 dir = player.position - transform.position;

        dir.Normalize();
        movement = dir;

    }

    private void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }
        enemyMove(movement);
    }

    public void enemyMove(Vector2 dir)
    {
        Vector2 position = enemyRb.position;

        if ((Vector2.Distance(transform.position, player.position) <= aggroRange && isAggro == false) || isAggro)
        {
            isAggro = true;
            enemyRb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));

            animator.SetFloat("moveX", direction);
            animator.SetFloat("moveY", 0);
 

        }
        else
        {
            if (vertical)
            {
                position.y = position.y + Time.deltaTime * speed * direction;
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
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-50);
        }
        else
        {
            vertical = !vertical;
        }

        
    }
    
    public void Fix()
    {
        broken = false;
        enemyRb.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
        hitEffect.Play();
        scoreText.text = $"{ GameManager.instance.score += 25}";
    }
}
