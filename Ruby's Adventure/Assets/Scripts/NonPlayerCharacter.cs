using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    public GameObject dialogBox2;
    float timerDisplay;

    private void Start()
    {
        dialogBox.SetActive(false);
        dialogBox2.SetActive(false);
        timerDisplay = -1.0f;
    }
    private void Update()
    {
        CheckIfDisplayed();
    }

    public void CheckIfDisplayed()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                
                dialogBox.SetActive(false);
                dialogBox2.SetActive(false);
            }
        }
    }

    public void DisplayDialog(int dialogue)
    {
        timerDisplay = displayTime;

        switch (dialogue)
        {
            case 1:
                dialogBox.SetActive(true);
                break;
            case 2:
                dialogBox2.SetActive(true);
                break;
        }   
        
    }
}
