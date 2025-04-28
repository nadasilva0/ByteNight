using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimpleDialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Assign the UI Text component in the Inspector
    public string[] dialogueLines; // Fill this array with dialogue lines in the Inspector
    public int[] whosTalkin; // Fill this array with dialogue lines in the Inspector
    private int currentLineIndex = 0;
    public string SceneName;

    
    private Color SkeetsColor = new Color(0.4f, 0.8f, 0.4f);
    private Color VanessaColor = new Color(0.6f, 0.4f, 0.8f);

    public Image Skeets;
    public Image Vanessa;
    public Image VanessaShocked;

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

    public void SkipDisShit()
    {
        dialogueBox.SetActive(false);
        GameObject.FindObjectOfType<SimpleSceneTransition>().ChangeScene("SampleScene");
    }

    private void ShowDialogue()
    {
        // Display the current dialogue line
        dialogueText.text = dialogueLines[currentLineIndex];

        // Change text color based on who is talking
        if (whosTalkin[currentLineIndex] == 0)
        {
            Vanessa.enabled = true;
            VanessaShocked.enabled = false;
            dialogueText.color = VanessaColor;
            Skeets.color = Color.gray;
            Vanessa.color = Color.white;
            VanessaShocked.color = Color.white;
        }
        else if (whosTalkin[currentLineIndex] == 1)
        {
            dialogueText.color = SkeetsColor;
            Skeets.color = Color.white;
            Vanessa.color = Color.gray;
            VanessaShocked.color = Color.gray;
        }
        else
        {
            Vanessa.enabled = false;
            VanessaShocked.enabled = true;
            dialogueText.color = VanessaColor;
            Skeets.color = Color.gray;
            Vanessa.color = Color.white;
            VanessaShocked.color = Color.white;
        }
    }
}
