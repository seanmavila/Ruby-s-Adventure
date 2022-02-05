using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCollectible : MonoBehaviour
{

    public AudioClip collectedClip;

    private Text scoreText;

    private void Start()
    {
        scoreText = GameObject.Find("Score Text").GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController controller = collision.GetComponent<PlayerController>();



        if (controller != null)
        {
            if (controller.health < controller.maxHealth)
            {
                controller.ChangeHealth(10);
            }
            Destroy(gameObject);
            controller.PlaySound(collectedClip);
            scoreText.text = $"{ GameManager.instance.score += 10}";

        }
    }
}
