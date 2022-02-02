using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 5;
    public float iFrameTime = 2.0f;

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
    private float horizontalMove;
    private float verticalMove;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        iFrameCheck();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMovement();
    }

    public void playerMovement()
    {
        position = playerRb.position;
        position.x += speed * horizontalMove * Time.deltaTime;
        position.y += speed * verticalMove * Time.deltaTime;
        playerRb.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible) return;
            isInvincible = true;
            iFrameTimer = iFrameTime;
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
}
