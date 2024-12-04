using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class PlayerLivesController : MonoBehaviour
{
    public int health = 10;
    public TMP_Text livesDisplay;

    // Update is called once per frame
    void Update()
    {
        if (health >0)
        {
            livesDisplay.text = $"Lives: {health}\n";
        }
        else
        {
            livesDisplay.text = $"You lose!\n";
        }
    }
}