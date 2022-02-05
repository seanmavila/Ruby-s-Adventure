using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollectible : MonoBehaviour
{
    public AudioClip collectedClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController controller = collision.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.ChangeAmmo(1);
            Destroy(gameObject);
            controller.PlaySound(collectedClip);
        }
    }
}
