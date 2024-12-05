using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ImageBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject textObject; // Assign the Text GameObject in the inspector

    private Vector3 originalImageScale;
    private Vector3 originalTextScale;
    public AudioClip clickSound;
    private AudioSource audioSource;

    void Start()
    {
        // Store the original scales
        originalImageScale = transform.localScale;

        if (textObject != null)
        {
            originalTextScale = textObject.transform.localScale;
        }

        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Enlarge the image
        transform.localScale = originalImageScale * 1.2f;

        // Enlarge the text, if assigned
        if (textObject != null)
        {
            textObject.transform.localScale = originalTextScale * 1.2f;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Reset the image size
        transform.localScale = originalImageScale;

        // Reset the text size, if assigned
        if (textObject != null)
        {
            textObject.transform.localScale = originalTextScale;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Change to the next scene when the image is clicked
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
  
        GameObject.FindObjectOfType<SimpleSceneTransition>().ChangeScene("Dialogue");
    }
}
