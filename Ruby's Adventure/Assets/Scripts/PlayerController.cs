using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentAmmo = 10;
    public float iFrameTime = 2.0f;
    public GameObject projectilePrefab;
    public ParticleSystem hitEffect;
    public AudioClip projectileClip;
    public AudioClip hitClip;
    public float restartLevelDelay = 1f;

    
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

    public int ammo
    {
        get { return currentAmmo; }
        set
        {
            if (currentAmmo < 0)
            {
                currentAmmo = 0;
                Debug.LogError("Ammo cannot be negative!");
            }
        }
    }

    [SerializeField] float speed = 3.0f;
    Rigidbody2D playerRb;
    Vector2 position;
    Vector2 lookDir = new Vector2(1, 0);
    Animator animator;
    bool isInvincible;
    float iFrameTimer;
    float horizontalMove;
    float verticalMove;
    AudioSource audioSource;
    bool isMoving = false;
    int currentHealth;
    bool energyJustConsumed;
    Text ammoText;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        ammoText = GameObject.Find("Ammo Text").GetComponent<Text>();
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

        CheckNPC();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMovement();
    }

    public void playerMovement()
    {
        Vector2 move = new Vector2(horizontalMove, verticalMove);

        if (horizontalMove != 0 || verticalMove != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

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
            PlaySound(hitClip);
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    public void ChangeAmmo(int amount)
    {
        currentAmmo += amount;
        ammoText.text = $"{currentAmmo}";
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
        
        if (currentAmmo > 0)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, playerRb.position + Vector2.up * 0.5f, Quaternion.identity);

            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDir, 300);
            animator.SetTrigger("Launch");
            PlaySound(projectileClip);

            ChangeAmmo(-1);
        }
    }

    public void CheckNPC()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(playerRb.position + Vector2.up * 0.2f, lookDir, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
