using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimpleDialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Assign the UI Text component in the Inspector
    public string[] dialogueLines; // Fill this array with dialogue lines in the Inspector
    private int currentLineIndex = 0;
    public string SceneName;

    
    public Color evenColor = new Color(0.6f, 0.4f, 0.8f); 
    public Color oddColor = new Color(0.4f, 0.8f, 0.4f); 

    public GameObject dialogueBox; // The dialogue box UI element

    private void Start()
    {
        // Make sure the dialogue box is active and show the first line
        dialogueBox.SetActive(true);
        ShowDialogue();
    }

    public void Click()
    {
        // Move to the next line of dialogue
        currentLineIndex++;

        if (currentLineIndex < dialogueLines.Length)
        {
            ShowDialogue();
        }
        else
        {
            // Hide the dialogue box when all lines are done
            dialogueBox.SetActive(false);
            GameObject.FindObjectOfType<SimpleSceneTransition>().ChangeScene(SceneName);

        }
    }

    private void ShowDialogue()
    {
        // Display the current dialogue line
        dialogueText.text = dialogueLines[currentLineIndex];

        // Change text color based on whether the index is even or odd
        if (currentLineIndex % 2 == 0)
        {
            dialogueText.color = evenColor;
        }
        else
        {
            dialogueText.color = oddColor;
        }
    }
}
