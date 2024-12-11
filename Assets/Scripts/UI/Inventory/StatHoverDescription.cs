using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatHoverDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject textBox;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textBox.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textBox.SetActive(false);
    }
}
