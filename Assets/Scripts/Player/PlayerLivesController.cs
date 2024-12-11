using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class PlayerLivesController : MonoBehaviour
{
    public int health = 10;
    public TMP_Text livesDisplay;
    public string sceneName; // Assign the scene name in the Inspector
    public bool hasLostLives = false;

    // Update is called once per frame
    void Update()
    {
        if (health >0)
        {
            livesDisplay.text = $"{health}\n";
        }
        else if (health <= 0 && health > -30)
        {
            livesDisplay.text = $"SHIT!!!!!\n";
            GameObject.FindObjectOfType<SimpleSceneTransition>().ChangeScene(sceneName);
            health = -69;
        }
    }
}