using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 3.0f;
    private Vector2 position;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
    }

    public void playerMovement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        position = transform.position;
        position.x += speed * horizontalMove * Time.deltaTime;
        position.y += speed * verticalMove * Time.deltaTime;
        transform.position = position;
    }
}
