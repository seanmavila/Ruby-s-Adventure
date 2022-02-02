using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 5;
    public float iFrameTime = 2.0f;
    public GameObject projectilePrefab;
    public ParticleSystem hitEffect;

    private int currentHealth;
    public int health
    {
        get { return currentHealth; }
        set
        {
            if (currentHealth < 0)
            {
                currentHealth = 0;
                Debug.LogError("Health cannot be negative!");
            }
            if(currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
                Debug.LogError("Helath cannot be greater than " + maxHealth + "!");
            }
        }
    }

    private bool isInvincible;
    private float iFrameTimer;

    [SerializeField] private float speed = 3.0f;
    private Rigidbody2D playerRb;
    private Vector2 position;
    private Vector2 lookDir = new Vector2(1, 0);
    private Animator animator;
    private float horizontalMove;
    private float verticalMove;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        iFrameCheck();

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMovement();
    }

    public void playerMovement()
    {
        Vector2 move = new Vector2(horizontalMove, verticalMove);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDir.Set(move.x, move.y);
            lookDir.Normalize();
        }

        animator.SetFloat("Look X", lookDir.x);
        animator.SetFloat("Look Y", lookDir.y);
        animator.SetFloat("Speed", move.magnitude);

        position = playerRb.position;
        position = position + move * speed * Time.deltaTime;
        playerRb.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible) return;
            isInvincible = true;
            iFrameTimer = iFrameTime;
            animator.SetTrigger("Hit");
            hitEffect.Play();
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    public void iFrameCheck()
    {
        if (isInvincible)
        {
            iFrameTimer -= Time.deltaTime;
            if (iFrameTimer < 0)
            {
                isInvincible = false;
            }
        }
    }

    private void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, playerRb.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDir, 300);

        animator.SetTrigger("Launch");
    }
}
