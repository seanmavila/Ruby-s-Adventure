using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCollectible : MonoBehaviour
{

    public AudioClip collectedClip;

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
            SoundManager.instance.PlaySingle(collectedClip);
            controller.ChangeScore(5);

        }
    }
}
